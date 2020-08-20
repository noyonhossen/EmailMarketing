using EmailMarketing.Common.Constants;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Enums;
using EmailMarketing.Framework.Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Contacts
{
    public class ContactExportModel : ContactsBaseModel
    {
        public bool IsExportAll{get;set;}
        public IList<ContactValueTextModel> GroupSelectList { get; set; }
        public string SendEmailAddress { get; set; }
        public bool IsSendEmailNotifyForAll { get; set; }
        public bool IsSendEmailNotifyForGroupwise { get; set; }

        public ContactExportModel(IContactExportService contactService,
           ICurrentUserService currentUserService) : base(contactService, currentUserService)
        {

        }
        public ContactExportModel() : base()
        {
            
        }
        public async Task<IList<ContactValueTextModel>> GetAllGroupDetailsAsync()
        {
            return (await _contactExportService.GetAllGroupAsync(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text, Count = x.Count, IsChecked = false }).ToList();
        }

        public async Task ExportAllContact()
        {
            if (IsSendEmailNotifyForAll == true && SendEmailAddress.Length ==0)
            {
                throw new Exception();
            }
            else
            {
                var downloadQueue = new DownloadQueue();
                downloadQueue.FileName = Guid.NewGuid().ToString() + ".xlsx";
                downloadQueue.FileUrl = ConstantsValue.AllContactExportFileUrl;
                downloadQueue.IsProcessing = true;
                downloadQueue.IsSucceed = false;
                downloadQueue.UserId = _currentUserService.UserId;
                downloadQueue.DownloadQueueFor = DownloadQueueFor.ContactAllExport;
                downloadQueue.IsSendEmailNotify = IsSendEmailNotifyForAll;
                downloadQueue.SendEmailAddress = SendEmailAddress;
                await _contactExportService.SaveDownloadQueueAsync(downloadQueue);

            }
        }

        public async Task ExportContactsGroupwise()
        {
            if (IsSendEmailNotifyForGroupwise == true && SendEmailAddress.Length == 0)
            {
                throw new Exception();
            }
            else
            {
                var downloadQueue = new DownloadQueue();
                downloadQueue.FileName = Guid.NewGuid().ToString() + ".xlsx";
                downloadQueue.FileUrl = ConstantsValue.GroupwiseContactExportFileUrl;
                downloadQueue.IsProcessing = true;
                downloadQueue.IsSucceed = false;
                downloadQueue.UserId = _currentUserService.UserId;
                downloadQueue.DownloadQueueFor = DownloadQueueFor.ContactGroupWiseExport;
                downloadQueue.IsSendEmailNotify = IsSendEmailNotifyForGroupwise;
                downloadQueue.SendEmailAddress = SendEmailAddress;
                await _contactExportService.SaveDownloadQueueAsync(downloadQueue);

                var dowloadQueueSubEntityList = new List<DownloadQueueSubEntity>();
                foreach (var item in GroupSelectList)
                {
                    if (item.IsChecked)
                    {
                        var dowloadQueueSubEntity = new DownloadQueueSubEntity();
                        dowloadQueueSubEntity.DownloadQueueId = downloadQueue.Id;
                        dowloadQueueSubEntity.DownloadQueueSubEntityId = item.Value;
                        dowloadQueueSubEntityList.Add(dowloadQueueSubEntity);
                    }
                }
                await _contactExportService.AddDownloadQueueSubEntitiesAsync(dowloadQueueSubEntityList);
            }
        }
    }
}
