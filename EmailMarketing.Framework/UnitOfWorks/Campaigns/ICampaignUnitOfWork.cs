using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Campaings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public interface ICampaignUnitOfWork : IUnitOfWork
    {
        public ICampaignRepository CampaignRepository { get; set; }
        public IEmailTemplateRepository EmailTemplateRepository { get; set; }
    }
}
