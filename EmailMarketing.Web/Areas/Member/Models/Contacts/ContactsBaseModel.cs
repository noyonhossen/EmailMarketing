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
        protected readonly IGroupService _groupService;
        protected readonly ICurrentUserService _currentUserService;

        public ContactsBaseModel(IContactExcelService contactExcel, IGroupService groupService,
            ICurrentUserService currentUserService)
        {
            _contactExcelService = contactExcel;
            _currentUserService = currentUserService;
        }

        public ContactsBaseModel()
        {
            _contactExcelService = Startup.AutofacContainer.Resolve<IContactExcelService>();
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
        }

        public void Dispose()
        {
            _contactExcelService?.Dispose();
        }
    }
}
