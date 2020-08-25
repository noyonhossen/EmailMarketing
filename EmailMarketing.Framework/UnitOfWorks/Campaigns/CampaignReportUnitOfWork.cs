using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public class CampaignReportUnitOfWork : UnitOfWork, ICampaignReportUnitOfWork
    {
        public ICampaignReportRepository CampaingReportRepository { get; set; }
        public CampaignReportUnitOfWork(FrameworkContext dbContext,
            ICampaignReportRepository campaingReportRepository) : base(dbContext)
        {
            this.CampaingReportRepository = campaingReportRepository;
        }
    }
}