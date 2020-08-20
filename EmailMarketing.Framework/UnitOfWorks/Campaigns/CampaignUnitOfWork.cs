using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public class CampaignUnitOfWork : EmailMarketing.Data.UnitOfWork, ICampaignUnitOfWork
    {
        public ICampaignRepository CampaignRepository { get; set; }
        public CampaignUnitOfWork(FrameworkContext dbContext) : base(dbContext)
        {

        }
    }
}
