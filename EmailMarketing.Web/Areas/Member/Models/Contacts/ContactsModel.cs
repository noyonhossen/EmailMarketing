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

        public ContactsModel(IContactUploadService contactUploadService,
            ICurrentUserService currentUserService) : base(contactUploadService, currentUserService)
        {

        }
        public ContactsModel() : base()
        {

        }

        public async Task<IList<Contact>> GetAllContactAsync()
        {
            return new List<Contact>();
        }
    }
}
