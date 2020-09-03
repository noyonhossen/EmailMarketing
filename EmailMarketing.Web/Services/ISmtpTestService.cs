using EmailMarketing.Framework.Entities.SMTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Services
{
    public interface ISmtpTestService
    {
        Task<bool> TestSmtpSettings(SMTPConfig sMTPConfig);
    }
}
