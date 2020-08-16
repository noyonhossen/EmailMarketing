using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.UnitOfWorks.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Services.Campaigns
{
    public class CampaignService : ICampaignService
    {
        private ICampaignUnitOfWork _campaignUnitOfWork;

        public CampaignService(ICampaignUnitOfWork campaignUnitOfWork)
        {
            _campaignUnitOfWork = campaignUnitOfWork;
        }

        public void Dispose()
        {
            _campaignUnitOfWork?.Dispose();
        }
    }
}
