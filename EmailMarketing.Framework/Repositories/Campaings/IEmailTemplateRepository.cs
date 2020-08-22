using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Campaings
{
    public interface IEmailTemplateRepository : IRepository<EmailTemplate, int, FrameworkContext>
    {
    }
}
