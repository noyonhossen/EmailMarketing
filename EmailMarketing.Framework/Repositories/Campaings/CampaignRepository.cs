using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Campaings
{
    public class CampaignRepository : Repository<Campaign, int, FrameworkContext>, ICampaignRepository
    {
        public CampaignRepository(FrameworkContext context) : base(context) { }
    }
}
