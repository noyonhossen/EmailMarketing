using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Campaings
{
    public class EmailTemplateRepository : Repository<EmailTemplate, int, FrameworkContext>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(FrameworkContext context) : base(context) { }
    }
}
