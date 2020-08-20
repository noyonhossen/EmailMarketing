﻿using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Campaigns
{
    public interface ICampaignReportExportService : IDisposable
    {
        Task<IList<(int Value, string CampaignName, string Email, bool IsDelivered, bool IsSeen, DateTime SendDateTime, DateTime? SeenDateTime)>> GetAllCampaignReportAsync(
            Guid? userId);
        Task SaveDownloadQueueAsync(DownloadQueue downloadQueue);
        Task<IList<DownloadQueue>> GetDownloadQueue();
        Task<DownloadQueue> GetDownloadQueueByIdAsync(int campaingReportId);
        Task UpdateDownloadQueueAync(DownloadQueue downloadQueue);
        Task AddDownloadQueueSubEntities(DownloadQueueSubEntity downloadQueueSubEntity);
        Task ExcelExportForAllCampaignAsync(DownloadQueue downloadQueue);
        Task ExcelExportForCampaignWiseAsync(DownloadQueue downloadQueue);
        Task<IList<(int Value, string CampaignName, string Email, bool IsDelivered, bool IsSeen, DateTime SendDateTime, DateTime? SeenDateTime)>> GetCampaignWiseReportAsync(Guid? userId, int campaignId);
    }
}
