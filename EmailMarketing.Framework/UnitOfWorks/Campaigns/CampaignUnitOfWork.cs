using EmailMarketing.Framework.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public class CampaignUnitOfWork : EmailMarketing.Data.UnitOfWork, ICampaignUnitOfWork
    {
        public CampaignUnitOfWork(FrameworkContext dbContext) : base(dbContext)
        {

        }
    }
}
