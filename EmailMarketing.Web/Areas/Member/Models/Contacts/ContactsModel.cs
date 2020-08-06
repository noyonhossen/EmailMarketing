using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Contacts
{
    public class ContactsModel : ContactsBaseModel
    {
        public IList<Contact> Contacts { get; set; }

        public ContactsModel(IContactExcelService contactExcelService,
            ICurrentUserService currentUserService) : base(contactExcelService, currentUserService)
        {

        }
        public ContactsModel() : base()
        {

        }

        public async Task<IList<Contact>> GetAllContactAsync()
        {
            return (await _contactExcelService.GetAllContactsAsync(_currentUserService.UserId));
        }
    }
}
