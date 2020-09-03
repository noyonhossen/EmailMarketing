using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.SMTP;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Areas.Admin.Models;
using EmailMarketing.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Smtp
{
    public class SMTPBaseModel:MemberBaseModel, IDisposable
    {
        protected readonly ISMTPService _smtpService;
        protected readonly IApplicationUserService _applicationUserService;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly ISmtpTestService _smtpTestService;

        public SMTPBaseModel(ISMTPService smtpService, IApplicationUserService applicationUserService,
            ICurrentUserService currentUserService, ISmtpTestService smtpTestService)
        {
            _smtpService = smtpService;
            _applicationUserService = applicationUserService;
            _currentUserService = currentUserService;
            _smtpTestService = smtpTestService;
        }

        public SMTPBaseModel()
        {
            _smtpService = Startup.AutofacContainer.Resolve<ISMTPService>();
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
            _smtpTestService = Startup.AutofacContainer.Resolve<ISmtpTestService>();
        }

        public void Dispose()
        {
            _smtpService?.Dispose();
        }
    }
}
