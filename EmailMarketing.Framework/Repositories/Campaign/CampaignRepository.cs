using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Campaign
{
    public class CampaignRepository : Repository<EmailMarketing.Framework.Entities.Campaigns.Campaign, int, FrameworkContext>, ICampaignRepository
    {
        public CampaignRepository(FrameworkContext dbContext) : base(dbContext)
        {

        }
    }
}
