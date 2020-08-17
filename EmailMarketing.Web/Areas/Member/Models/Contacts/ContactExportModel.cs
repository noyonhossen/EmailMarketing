using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities;
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
        public IList<ContactValueTextModel> GroupSelectList { get; set; }
        public ContactExportModel(IContactService contactService,
           ICurrentUserService currentUserService) : base(contactService, currentUserService)
        {

        }
        public ContactExportModel() : base()
        {

        }
        public async Task<IList<ContactValueTextModel>> GetAllGroupForSelectAsync()
        {
            return (await _contactService.GetAllGroupsAsync(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text, Count = x.Count, IsChecked = false }).ToList();
        }

        public async Task CheckSelectOption()
        {
            var downloadQueue = new DownloadQueue();
            //await
        }
    }
}
