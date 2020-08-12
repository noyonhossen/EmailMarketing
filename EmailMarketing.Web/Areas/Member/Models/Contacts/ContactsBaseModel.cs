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
        protected readonly IContactExcelService _contactExcelService;
        protected readonly ICurrentUserService _currentUserService;
        protected  IContactService _contactService;
             
        public ContactsBaseModel(IContactExcelService contactExcel,
            ICurrentUserService currentUserService)
        {
            _contactExcelService = contactExcel;
            _currentUserService = currentUserService;
        }
        public ContactsBaseModel(IContactService contactService,
           ICurrentUserService currentUserService)
        {
            _contactService = contactService;
            _currentUserService = currentUserService;
        }

        public ContactsBaseModel()
        {
            _contactExcelService = Startup.AutofacContainer.Resolve<IContactExcelService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
        }

        public void Dispose()
        {
            _contactExcelService?.Dispose();
        }
    }
}
