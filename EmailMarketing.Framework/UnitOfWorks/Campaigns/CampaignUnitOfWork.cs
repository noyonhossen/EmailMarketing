using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public class CampaignUnitOfWork : EmailMarketing.Data.UnitOfWork, ICampaignUnitOfWork
    {
        public ICampaingRepository CampaingRepository { get; set; }
        public CampaignUnitOfWork(FrameworkContext dbContext,
            ICampaingRepository campaingRepository) : base(dbContext)
        {
            this.CampaingRepository = campaingRepository;
        }
    }
}