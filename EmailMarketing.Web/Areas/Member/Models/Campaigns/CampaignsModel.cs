using ClosedXML.Excel;
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
        public bool IsExportAll { get; set; }
        public IList<CampaignValueTextModel> CampaignReportList { get; set; }
        public CampaignsModel(ICampaignReportExportService campaignService,
            ICurrentUserService currentUserService) : base(campaignService, currentUserService)
        {

        }
        public CampaignsModel() : base()
        {

        }

        public async Task CheckSelectOption()
        {
            var downloadQueue = new DownloadQueue();
            downloadQueue.FileName = "AllCampaignReports.xlsx";
            downloadQueue.FileUrl = "D:\\Working";
            downloadQueue.IsProcessing = true;
            downloadQueue.IsSucceed = false;
            downloadQueue.UserId = _currentUserService.UserId;
            downloadQueue.DownloadQueueFor = DownloadQueueFor.CampaignAllReportExport;
            downloadQueue.IsSendEmailNotify = true;
            downloadQueue.SendEmailAddress = "alpha.bug.debuger@gmail.com";
            await _campaignService.SaveDownloadQueueAsync(downloadQueue);
        }
    }
}
