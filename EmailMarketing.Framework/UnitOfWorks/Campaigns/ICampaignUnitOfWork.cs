using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public interface ICampaignUnitOfWork : IUnitOfWork
    {
        ICampaignReportRepository CampaignReportRepository { get; set; }
        public ICampaignRepository CampaignRepository { get; set; }
        public IEmailTemplateRepository EmailTemplateRepository { get; set; }
    }
}
