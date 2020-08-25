using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Campaigns
{

    public class CampaignBaseModel : MemberBaseModel, IDisposable
    {
        protected readonly ICampaignService _campaignService;
        protected readonly ICampaignReportExportService _campaignREService;
        protected readonly ICurrentUserService _currentUserService;
        public CampaignBaseModel(ICampaignService campaignService, ICampaignReportExportService campaignREService,

            ICurrentUserService currentUserService)
        {
            _campaignService = campaignService;
            _campaignREService = campaignREService;
            _currentUserService = currentUserService;
        }

        public CampaignBaseModel()
        {
            _campaignService = Startup.AutofacContainer.Resolve<ICampaignService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
            _campaignREService = Startup.AutofacContainer.Resolve<ICampaignReportExportService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
        }
        public void Dispose()
        {
            _campaignService?.Dispose();
        }
    }
}
