using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Campaigns
{
    public class CampaingReportRepository : Repository<CampaignReport, int, FrameworkContext>, ICampaingReportRepository
    {
        public CampaingReportRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
