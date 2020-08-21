using Autofac;
using EmailMarketing.Framework.Services.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Campaigns
{
    public class CampaignBaseModel : MemberBaseModel 
    {
        protected readonly ICampaignService _campaignService;
        public CampaignBaseModel(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }
        public CampaignBaseModel()
        {
            _campaignService = Startup.AutofacContainer.Resolve<ICampaignService>();
        }
        
    }
}
