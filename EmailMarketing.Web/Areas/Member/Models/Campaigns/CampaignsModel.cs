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

namespace EmailMarketing.Web.Areas.Member.Models.Campaigns
{
    public class CampaignsModel : CampaignBaseModel
    {
        public int Id { get; set; }
        public bool IsExportAll { get; set; }
        //public IList<CampaignValueTextModel> CampaignReportList { get; set; }
        public IList<CampaignReportValueTextModel> CampaignSelectList { get; set; }
        public string SendEmailAddress { get; set; }
        public bool IsSendEmailNotifyForAll { get; set; }
        public bool IsSendEmailNotifyForGroupwise { get; set; }
        public CampaignsModel(ICampaignReportExportService campaignService,
            ICurrentUserService currentUserService) : base(campaignService, currentUserService)
        {

        }
        public CampaignsModel() : base()
        {

        }
        //public async Task<IList<CampaignReportValueTextModel>> GetAllCampaignDetailsAsync()
        //{
        //    return (await _campaignService.GetAllCampaignsAsync(_currentUserService.UserId))
        //                                   .Select(x => new CampaignReportValueTextModel { Value = x.Value, Text = x.Text, Count = x.Count, IsChecked = false }).ToList();
        //}

        public async Task ExportAllCampaign()
        {
            if (IsSendEmailNotifyForAll == true && SendEmailAddress.Length == 0)
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
            if (IsSendEmailNotifyForGroupwise == true && SendEmailAddress.Length == 0)
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
                downloadQueue.IsSendEmailNotify = IsSendEmailNotifyForGroupwise;
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
