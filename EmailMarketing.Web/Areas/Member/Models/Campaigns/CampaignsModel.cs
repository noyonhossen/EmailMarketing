using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.Campaigns;
namespace EmailMarketing.Web.Areas.Member.Models.Campaigns
{
    public class CampaignsModel : CampaignBaseModel
    {
        public int CampaignId { get; set; } = 2;
        public CampaignsModel(ICampaignService campaignService, ICurrentUserService currentUserService) : base(campaignService,currentUserService) { }
        public CampaignsModel() : base() { }

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
                            item.Name,
                            item.IsProcessing ? "Yes" : "No",
                            item.CampaignReports.Count.ToString(),
                            item.SendDateTime.ToString(),
                            item.SendEmailAddress.ToString(),
                            item.Id.ToString()

                        }).ToArray()
            };
        }

        internal void SetCapaignId(int id)
        {
            this.CampaignId = id;
        }

        public async Task<object> GetCampaignReportByCampaignIdAsync(DataTablesAjaxRequestModel tableModel, int CampaignId)
         {
            
            var result = await _campaignService.GetAllCampaignReportAsync(
                _currentUserService.UserId,
                CampaignId ,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "CampaignName","Email" }),
                tableModel.PageIndex, tableModel.PageSize);

            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,
                data = (from item in result.Items
                        select new string[]
                        {
                            item.Contact.Email.ToString(),
                            item.IsDelivered.ToString(),
                            item.IsSeen ? "Yes" : "No",
                            item.SeenDateTime.ToString(),
                            item.SendDateTime.ToString()

                        }).ToArray()
            };
        }


    }
}

