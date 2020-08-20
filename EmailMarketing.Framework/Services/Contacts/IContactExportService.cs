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
        Task<Contact> GetContactByIdAsync(int contactId);
        Task<IList<ContactGroup>> GetAllGroupsByIdAsync(Guid? userId,int groupId);
        Task SaveDownloadQueueAsync(DownloadQueue downloadQueue);
        Task UpdateDownloadQueueAsync(DownloadQueue downloadQueue);
        Task<IList<DownloadQueue>> GetDownloadQueueAsync();
        Task<DownloadQueue> GetDownloadQueueByIdAsync(int contactUploadId);
        Task AddDownloadQueueSubEntitiesAsync(IList<DownloadQueueSubEntity> downloadQueueSubEntities);
        Task ExcelExportForAllContactsAsync(DownloadQueue downloadQueue);
        Task ExcelExportForGroupwiseContactsAsync(DownloadQueue downloadQueue);
    }
}