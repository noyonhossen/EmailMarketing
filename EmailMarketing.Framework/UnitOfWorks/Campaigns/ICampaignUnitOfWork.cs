using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public interface ICampaignUnitOfWork : IUnitOfWork
    {
        ICampaignRepository CampaignRepository { get; set; }
    }
}
