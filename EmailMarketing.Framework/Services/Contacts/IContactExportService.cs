using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Contacts
{
    public interface IContactExportService : IDisposable
    {

        Task<IList<(int Value, string Text, int Count)>> GetAllGroupsAsync(Guid? userId);
        Task<IList<Contact>> GetAllContactsAsync(Guid? userId);
        Task<Contact> GetContactById(int contactId);
        Task<IList<ContactGroup>> GetAllGroupsByIdAsync(int groupId);
        Task SaveDownloadQueueAsync(DownloadQueue downloadQueue);
        Task<IList<DownloadQueue>> GetDownloadQueue();
        Task<DownloadQueue> GetDownloadQueueByIdAsync(int contactUploadId);
        Task AddDownloadQueueSubEntities(IList<DownloadQueueSubEntity> downloadQueueSubEntities);
        Task UpdateDownloadQueueAync(DownloadQueue downloadQueue);
    }
}