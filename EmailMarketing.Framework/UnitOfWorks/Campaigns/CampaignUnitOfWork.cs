using EmailMarketing.Framework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public class CampaignUnitOfWork : EmailMarketing.Data.UnitOfWork, ICampaignUnitOfWork
    {
        public CampaignUnitOfWork(FrameworkContext dbContext) : base(dbContext)
        {

        }
    }
}