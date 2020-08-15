﻿using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.SMTP;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Areas.Admin.Models;
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

        public SMTPBaseModel(ISMTPService smtpService, IApplicationUserService applicationUserService,
            ICurrentUserService currentUserService)
        {
            _smtpService = smtpService;
            _applicationUserService = applicationUserService;
            _currentUserService = currentUserService;
        }

        public SMTPBaseModel()
        {
            _smtpService = Startup.AutofacContainer.Resolve<ISMTPService>();
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
        }

        public void Dispose()
        {
            _smtpService?.Dispose();
        }
    }
}
