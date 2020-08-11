using EmailMarketing.Common.Exceptions;
using EmailMarketing.Common.Extensions;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.UnitOfWork.Contacts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Contacts
{
    public class ContactService : IContactService
    {
        private IContactUnitOfWork _contactUnitOfWork;

        public ContactService(IContactUnitOfWork contactUnitOfWork)
        {
            _contactUnitOfWork = contactUnitOfWork;
        }
        public async Task<(IList<Contact> Items, int Total, int TotalFilter)> GetAllContactAsync(
            Guid? userId,string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Entities.Contacts.Contact, object>>>()
            {
                ["Email"] = v => v.Email
            };
            var result = await _contactUnitOfWork.ContactRepository.GetAsync<Entities.Contacts.Contact>(
                x => x, x => (!userId.HasValue || x.UserId == userId.Value)&& (x.Email.Contains(searchText)),
                x => x.ApplyOrdering(columnsMap, orderBy), x=> x.Include(i=> i.ContactGroups).ThenInclude(i=>i.Group),
                pageIndex, pageSize, true);
            return (result.Items, result.Total, result.TotalFilter);
        }
        public async Task<Contact> GetByIdAsync(int id)
        {
            var result = await _contactUnitOfWork.ContactRepository.GetFirstOrDefaultAsync(
                x => x, x => x.Id == id,
                x => x.Include(i => i.ContactGroups).ThenInclude(i => i.Group)
                        .Include(i => i.ContactValueMaps).ThenInclude(i => i.FieldMap));

            if (result == null) throw new NotFoundException(nameof(Contact), id);

            return result;
        }
        public async Task<Contact> DeleteAsync(int id)
        {
            var contact = await GetByIdAsync(id);
            if (contact == null) throw new NotFoundException(nameof(Contact), id);
            await _contactUnitOfWork.ContactRepository.DeleteAsync(id);
            await _contactUnitOfWork.SaveChangesAsync();
            return contact;
        }
        public void Dispose()
        {
            _contactUnitOfWork?.Dispose();
        }
    }
}
