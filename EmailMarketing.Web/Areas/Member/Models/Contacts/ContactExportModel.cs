using EmailMarketing.Common.Constants;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Enums;
using EmailMarketing.Framework.Services.Contacts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Contacts
{
    public class ContactExportModel : ContactsBaseModel
    {
        public bool IsExportAll { get; set; }
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
            if (IsSendEmailNotifyForAll == true && string.IsNullOrWhiteSpace(SendEmailAddress))
            {
                throw new Exception("Please Provide Email");
            }
            //To create directory if not exist
            if (Directory.Exists(ConstantsValue.AllContactExportFileUrl) == false)
            {
                DirectoryInfo directory = Directory.CreateDirectory(ConstantsValue.AllContactExportFileUrl);
            }
            try
            {
                var userId = _currentUserService.UserId;
                var distinctiveFileName = Guid.NewGuid().ToString();
                var downloadQueue = new DownloadQueue();
                downloadQueue.FileName = "AllContacts_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";
                downloadQueue.FileUrl = Path.Combine(ConstantsValue.AllContactExportFileUrl, distinctiveFileName) + Path.GetExtension(downloadQueue.FileName);
                downloadQueue.IsProcessing = true;
                downloadQueue.IsSucceed = false;
                downloadQueue.Created = DateTime.Now;
                downloadQueue.CreatedBy = userId;
                downloadQueue.UserId = userId;
                downloadQueue.DownloadQueueFor = DownloadQueueFor.ContactAllExport;
                downloadQueue.IsSendEmailNotify = IsSendEmailNotifyForAll;
                downloadQueue.SendEmailAddress = SendEmailAddress;
                await _contactExportService.SaveDownloadQueueAsync(downloadQueue);
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to export. Please try again.");
            }

        }

        public async Task ExportContactsGroupwise()
        {

            if (IsSendEmailNotifyForGroupwise == true && string.IsNullOrWhiteSpace(SendEmailAddress))
            {
                throw new Exception("Please Provide Email");
            }

            if (this.GroupSelectList == null) throw new Exception("Please add atleast one group to add contact.");
            else if (!this.GroupSelectList.Any(x => x.IsChecked)) throw new Exception("Please select at least one group.");

            //To create directory if not exist
            if (Directory.Exists(ConstantsValue.GroupwiseContactExportFileUrl) == false)
            {
                DirectoryInfo directory = Directory.CreateDirectory(ConstantsValue.GroupwiseContactExportFileUrl);
            }
            try
            {
                var userId = _currentUserService.UserId;
                var distinctiveFileName = Guid.NewGuid().ToString();
                var downloadQueue = new DownloadQueue();
                downloadQueue.FileName = "GroupwiseContacts_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";
                downloadQueue.FileUrl = Path.Combine(ConstantsValue.GroupwiseContactExportFileUrl, distinctiveFileName) + Path.GetExtension(downloadQueue.FileName); ;
                downloadQueue.IsProcessing = true;
                downloadQueue.IsSucceed = false;
                downloadQueue.Created = DateTime.Now;
                downloadQueue.CreatedBy = userId;
                downloadQueue.UserId = userId;
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
            catch(Exception ex)
            {
                throw new Exception("Failed to export. Please try again.");
            }
        }
    }
}
