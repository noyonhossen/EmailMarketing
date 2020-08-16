using EmailMarketing.Framework.Entities.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Services.Campaigns
{
    public interface IEmailTemplateService : IDisposable
    {
        void AddEmailTemplateAsync(EmailTemplate emailTemplate);
    }
}
