using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.UnitOfWorks.Contacts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Contacts
{
    public class ContactExportService : IContactExportService
    {
        private IContactUnitOfWork _contactUnitOfWork;
        public ContactExportService(IContactUnitOfWork contactUnitOfWork)
        {
            _contactUnitOfWork = contactUnitOfWork;
        }

        public async Task<IList<Contact>> GetAllContactAsync(Guid? userId)
        {
            var contacts = await _contactUnitOfWork.ContactRepository.GetAsync<Contact>(
                x => x, x => (!userId.HasValue || x.UserId == userId.Value), null, x => x.Include(i => i.ContactValueMaps).ThenInclude(i => i.FieldMap)
                .Include(i => i.ContactGroups).ThenInclude(i => i.Group), true
                );
            return contacts;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

       
    }
}
