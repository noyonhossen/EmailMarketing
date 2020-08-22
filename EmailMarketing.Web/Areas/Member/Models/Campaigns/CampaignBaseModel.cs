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

        public async Task<object> GetAllCampaignsAsync(DataTablesAjaxRequestModel tableModel)
        {

            var result = await _campaignService.GetAllCampaignAsync(
                _currentUserService.UserId,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Name" }),
                tableModel.PageIndex, tableModel.PageSize);

            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,
                data = (from item in result.Items
                        select new string[]
                        {
                                //item.Contact.Email.ToString(),
                                //item.IsDelivered ? "Yes" : "No",
                                //item.IsSeen ? "Yes" : "No",
                                //item.SeenDateTime.ToString(),
                                //item.SendDateTime.ToString()

                        }).ToArray()
            };
        }

       
    }
}
