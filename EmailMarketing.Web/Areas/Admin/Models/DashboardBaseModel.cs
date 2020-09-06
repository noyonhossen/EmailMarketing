using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.Campaigns;
using EmailMarketing.Framework.Services.Contacts;
using EmailMarketing.Framework.Services.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class DashboardBaseModel : AdminBaseModel, IDisposable
    {
        protected readonly ICampaignService _campaignService;
        protected readonly IContactService _contactService;
        protected readonly IGroupService _groupService;
        public DashboardBaseModel(ICampaignService campaignService, IContactService contactService,
            IGroupService groupUserService)
        {
            _campaignService = campaignService;
            _contactService = contactService;
            _groupService = groupUserService;
        }

        public DashboardBaseModel(ICampaignService campaignService,
            ICurrentUserService currentUserService)
        {
            _campaignService = campaignService;
            _currentUserService = currentUserService;
        }

        public DashboardBaseModel()
        {
            _campaignService = Startup.AutofacContainer.Resolve<ICampaignService>();
            _contactService = Startup.AutofacContainer.Resolve<IContactService>();
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }
        public void Dispose()
        {
            _campaignService?.Dispose();
            _contactService?.Dispose();
            _groupService?.Dispose();
        }
    }
}
