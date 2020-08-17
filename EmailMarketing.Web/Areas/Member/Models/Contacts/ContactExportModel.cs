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
        public ContactExportModel(IContactExportService contactService,
           ICurrentUserService currentUserService) : base(contactService, currentUserService)
        {

        }
        public ContactExportModel() : base()
        {
            
        }
        public async Task<IList<ContactValueTextModel>> GetAllGroupForSelectAsync()
        {
            return (await _contactExportService.GetAllGroupsAsync(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text, Count = x.Count, IsChecked = false }).ToList();
        }

        public async Task CheckSelectOption()
        {
            var downloadQueue = new DownloadQueue();
            downloadQueue.FileName = "AllContacts";
            downloadQueue.FileUrl = "D:\\Working";
            downloadQueue.IsProcessing = true;
            downloadQueue.IsSucceed = false;
            downloadQueue.DownloadQueueFor = DownloadQueueFor.ContactAllExport;
            downloadQueue.IsSendEmailNotify = true;
            downloadQueue.SendEmailAddress = "alpha.bug.debuger@gmail.com";
            await _contactExportService.SaveDownloadQueueAsync(downloadQueue);
        }
    }
}
