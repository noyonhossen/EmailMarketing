using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.Contacts;
using EmailMarketing.Framework.Services.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Contacts
{
    public class ContactsBaseModel : MemberBaseModel, IDisposable
    {
        protected readonly IContactUploadService _contactUploadService;
        protected readonly ICurrentUserService _currentUserService;

        public ContactsBaseModel(IContactUploadService contactUpload,
            ICurrentUserService currentUserService)
        {
            _contactUploadService = contactUpload;
            _currentUserService = currentUserService;
        }

        public ContactsBaseModel()
        {
            _contactUploadService = Startup.AutofacContainer.Resolve<IContactUploadService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
        }

        public void Dispose()
        {
            _contactUploadService?.Dispose();
        }
    }
}
