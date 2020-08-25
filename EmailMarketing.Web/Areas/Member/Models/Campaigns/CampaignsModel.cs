using ClosedXML.Excel;
using EmailMarketing.Common.Constants;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.Enums;
using EmailMarketing.Framework.Services.Campaigns;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.Campaigns;
namespace EmailMarketing.Web.Areas.Member.Models.Campaigns
{
    public class CampaignsModel : CampaignBaseModel
    {
        public int CampaignId { get; set; }

        public int Id { get; set; }
        public bool IsExportAll { get; set; }
        public string SendEmailAddress { get; set; }
        public bool IsSendEmailNotifyForAll { get; set; }
        public bool IsSendEmailNotifyForCampaignwise { get; set; }
        public IList<object> CampaignSelectList { get; set; }
        public CampaignsModel(ICampaignReportExportService campaignService,
            ICurrentUserService currentUserService) : base(campaignService, currentUserService)
        {

        }
        public CampaignsModel() : base()
        {

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
                            item.Name,
                            item.IsProcessing ? "Yes" : "No",
                            item.CampaignReports.Count().ToString(),
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
                            item.IsDelivered ? "Yes" : "No",
                            item.IsSeen ? "Yes" : "No",
                            item.SeenDateTime.ToString(),
                            item.SendDateTime.ToString()

                        }).ToArray()
            };
        }
        public async Task LoadAllCampaignSelectListAsync()
        { 
            this.CampaignSelectList = await _campaignService.GetCampaignsForSelectAsync(_currentUserService.UserId);
        }
        public async Task ExportAllCampaign()
        {
            if (IsSendEmailNotifyForAll == true && string.IsNullOrWhiteSpace(SendEmailAddress))
            {
                throw new Exception();
            }
            else
            {
                var downloadQueue = new DownloadQueue();
                downloadQueue.FileName = Guid.NewGuid().ToString() + ".xlsx";
                downloadQueue.FileUrl = ConstantsValue.AllCampaignReportExportFileUrl;
                downloadQueue.IsProcessing = true;
                downloadQueue.IsSucceed = false;
                downloadQueue.UserId = _currentUserService.UserId;
                downloadQueue.DownloadQueueFor = DownloadQueueFor.CampaignAllReportExport;
                downloadQueue.IsSendEmailNotify = IsSendEmailNotifyForAll;
                downloadQueue.SendEmailAddress = SendEmailAddress;
                await _campaignService.SaveDownloadQueueAsync(downloadQueue);
            }
        }

        public async Task ExportCampaignWise()
        {
            if (IsSendEmailNotifyForCampaignwise == true && string.IsNullOrWhiteSpace(SendEmailAddress))
            {
                throw new Exception();
            }
            else
            {
                var downloadQueue = new DownloadQueue();
                downloadQueue.FileName = Guid.NewGuid().ToString() + ".xlsx";
                downloadQueue.FileUrl = ConstantsValue.CampaignwiseReportExportFileUrl;
                downloadQueue.IsProcessing = true;
                downloadQueue.IsSucceed = false;
                downloadQueue.UserId = _currentUserService.UserId;
                downloadQueue.DownloadQueueFor = DownloadQueueFor.CampaignDetailsReportExport;
                downloadQueue.IsSendEmailNotify = IsSendEmailNotifyForCampaignwise;
                downloadQueue.SendEmailAddress = SendEmailAddress;
                await _campaignService.SaveDownloadQueueAsync(downloadQueue);

                var dowloadQueueSubEntity = new DownloadQueueSubEntity();
                dowloadQueueSubEntity.DownloadQueueId = downloadQueue.Id;
                dowloadQueueSubEntity.DownloadQueueSubEntityId = this.Id;
   
                await _campaignService.AddDownloadQueueSubEntities(dowloadQueueSubEntity);
            }
        }
    }
}

