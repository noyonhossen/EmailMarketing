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

            //var result =  await _contactUnitOfWork.ContactRepository.GetAsync(x => x,
            //     x => !x.IsDeleted && x.IsActive && (!userId.HasValue || x.Group.UserId == userId.Value),
            //     x => x.OrderByDescending(o => o.Created),
            //     x => x.Include(o => o.Group).Include(o => o.ContactValueMaps).ThenInclude(o => o.FieldMap),
            //     true);

            var columnsMap = new Dictionary<string, Expression<Func<Entities.Contacts.Contact, object>>>()
            {
                ["Email"] = v => v.Email
            };
            var result = await _contactUnitOfWork.ContactRepository.GetAsync<Entities.Contacts.Contact>(
                x => x, x => x.UserId == userId,
                x => x.ApplyOrdering(columnsMap, orderBy), null,
                pageIndex, pageSize, true);
            return (result.Items, result.Total, result.TotalFilter);
        }
        public void Dispose()
        {
            _contactUnitOfWork?.Dispose();
        }
    }
}
