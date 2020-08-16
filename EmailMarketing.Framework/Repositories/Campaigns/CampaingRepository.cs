using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Campaigns
{
    public class CampaingRepository : Repository<CampaignReport, int, FrameworkContext>,ICampaingRepository
    {
        public CampaingRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
