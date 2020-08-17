using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.Services.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Campaigns
{
    public class CampaignsModel : CampaignBaseModel
    {
        public IList<CampaignValueTextModel> CampaignReportList { get; set; }
        public CampaignsModel(ICampaignService campaignService,
            ICurrentUserService currentUserService) : base(campaignService, currentUserService)
        {

        }
        public CampaignsModel() : base()
        {

        }
        public async Task<IList<CampaignValueTextModel>> GetAllGroupForSelectAsync()
        {
            return (await _campaignService.GetAllCampaignReportAsync(_currentUserService.UserId))
                                           .Select(x => new CampaignValueTextModel { Value = x.Value, CampaignName = x.CampaignName, Email = x.Email,IsDelivered = x.IsDelivered, IsSeen = x.IsSeen, SendDateTime = x.SendDateTime, SeenDateTime = x.SeenDateTime }).ToList();

        }
    }
}
