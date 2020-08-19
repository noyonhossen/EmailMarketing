using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Repositories.Contacts;
using EmailMarketing.Framework.UnitOfWorks.Contacts;
using EmailMarketing.Framework.UnitOfWorks.Groups;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Contacts
{
    public class ContactExportService : IContactExportService
    {
        private IContactExportUnitOfWork _contactExportUnitOfWork;
        private IContactUnitOfWork _contactUnitOfWork;
        private IGroupUnitOfWork _groupUnitOfWork;
        public ContactExportService(IContactExportUnitOfWork contactExportUnitOfWork, IContactUnitOfWork contactUnitOfWork, IGroupUnitOfWork groupUnitOfWork)
        {
            _contactExportUnitOfWork = contactExportUnitOfWork;
            _contactUnitOfWork = contactUnitOfWork;
            _groupUnitOfWork = groupUnitOfWork;
        }

        public async Task<IList<Contact>> GetAllContactsAsync(Guid? userId)
        {
            var contacts = await _contactUnitOfWork.ContactRepository.GetAsync<Contact>(
                x => x, x => (!userId.HasValue || x.UserId == userId.Value), null, x => x.Include(i => i.ContactValueMaps).ThenInclude(i => i.FieldMap)
                .Include(i => i.ContactGroups).ThenInclude(i => i.Group), true
                );
            return contacts;
        }

        public async Task<IList<ContactGroup>> GetAllGroupsByIdAsync(int groupId)
        {
            var contacts = await _contactUnitOfWork.GroupContactRepository.GetAsync<ContactGroup>(
                x => x, x => (x.GroupId == groupId), null, null, true
                );
            return contacts;
        }
        public async Task<Contact>  GetContactById(int contactId)
        {
            var contact = await _contactUnitOfWork.ContactRepository.GetByIdAsync(contactId);
            return contact;
        }
        public async Task<IList<DownloadQueue>> GetDownloadQueue()
        {
            var result = await _contactExportUnitOfWork.DownloadQueueRepository.GetAsync(x => x, x => x.IsProcessing == true && x.IsSucceed ==false, null, null, true);
            return result;
        }

        public async Task<DownloadQueue> GetDownloadQueueByIdAsync(int contactUploadId)
        {
            var contactUpload = await _contactExportUnitOfWork.DownloadQueueRepository.GetFirstOrDefaultAsync(x => x, x => x.Id == contactUploadId,
                                    null, true);

            return contactUpload;
        }
        public async Task UpdateDownloadQueueAync(DownloadQueue downloadQueue)
        {
            await _contactExportUnitOfWork.DownloadQueueRepository.UpdateAsync(downloadQueue);
            await _contactExportUnitOfWork.SaveChangesAsync();
        }
        public void Dispose()
        {
            _contactUnitOfWork.Dispose();
        }

        public async Task<IList<(int Value, string Text, int Count)>> GetAllGroupsAsync(Guid? userId)
        {
            return (await _groupUnitOfWork.GroupRepository.GetAsync(x => new { Value = x.Id, Text = x.Name, Count = x.ContactGroups.Count() },
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.UserId == userId.Value), x => x.OrderBy(o => o.Name), null, true))
                                                   .Select(x => (Value: x.Value, Text: x.Text, Count: x.Count)).ToList();
        }
        public async Task SaveDownloadQueueAsync(DownloadQueue downloadQueue)
        {
            await _contactExportUnitOfWork.DownloadQueueRepository.AddAsync(downloadQueue);
            await _contactExportUnitOfWork.SaveChangesAsync();
        }
        public async Task AddDownloadQueueSubEntities(IList<DownloadQueueSubEntity> downloadQueueSubEntities)
        {
            await _contactExportUnitOfWork.DownloadQueueSubEntityRepository.AddRangeAsync(downloadQueueSubEntities);
            await _contactExportUnitOfWork.SaveChangesAsync();
        }

    }
}
