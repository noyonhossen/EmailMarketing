using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.UnitOfWorks.Campaigns;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Campaigns
{
    public class CampaignReportExportService : ICampaignReportExportService
    {
        private ICampaignReportExportUnitOfWork _campaignReportExportUnitOfWork;
        private ICampaignReportUnitOfWork _campaignReportUnitOfWork;
        public CampaignReportExportService(ICampaignReportExportUnitOfWork campaignReportExportUnitOfWork, ICampaignReportUnitOfWork campaignReportUnitOfWork)
        {
            _campaignReportExportUnitOfWork = campaignReportExportUnitOfWork;
            _campaignReportUnitOfWork = campaignReportUnitOfWork;
        }
        public async Task<IList<(int Value, string CampaignName, string Email, bool IsDelivered, bool IsSeen, DateTime SendDateTime, DateTime? SeenDateTime)>> GetAllCampaignReportAsync(Guid? userId)
        {
            var result = (await _campaignReportUnitOfWork.CampaingReportRepository.GetAsync(x => new { Value = x.Id, CampaignName = x.Campaign.Name, Email = x.Contact.Email, IsDelivered = x.IsDelivered, IsSeen = x.IsSeen, SendDateTime = x.SendDateTime, SeenDateTime = x.SeenDateTime },
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.Campaign.UserId == userId.Value), x => x.OrderBy(o => o.Contact.UserId), null, true))
                                                   .Select(x => (Value: x.Value, CampaignName: x.CampaignName, Email: x.Email, IsDelivered: x.IsDelivered, IsSeen: x.IsSeen, SendDateTime: x.SendDateTime, SeenDateTime: x.SeenDateTime)).ToList();
            if (result == null) throw new NotFoundException(nameof(CampaignReport), userId);
            return result;
        }

        public async Task<IList<CampaignReport>> GetAllCampaignsReportAsync(Guid? userId)
        {
            var campaignReports = await _campaignReportUnitOfWork.CampaingReportRepository.GetAsync<CampaignReport>(
                x => x, x => (!userId.HasValue || x.Campaign.UserId == userId.Value), null, x => x.Include(i => i.Contact), true
                );
            return campaignReports;
        }
        public async Task<IList<DownloadQueue>> GetDownloadQueue()
        {
            var result = await _campaignReportExportUnitOfWork.CampaignReportExportRepository.GetAsync(x => x, x => x.IsProcessing == true && x.IsSucceed == false, null, null, true);
            return result;
        }

        public async Task<DownloadQueue> GetDownloadQueueByIdAsync(int campaingReportId)
        {
            var contactUpload = await _campaignReportExportUnitOfWork.CampaignReportExportRepository.GetFirstOrDefaultAsync(x => x, x => x.Id == campaingReportId,
                                    null, true);
            return contactUpload;
        }
        public async Task SaveDownloadQueueAsync(DownloadQueue downloadQueue)
        {
            await _campaignReportExportUnitOfWork.CampaignReportExportRepository.AddAsync(downloadQueue);
            await _campaignReportExportUnitOfWork.SaveChangesAsync();
        }
        public void Dispose()
        {
            _campaignReportUnitOfWork?.Dispose();
        }

        public async Task UpdateDownloadQueue(DownloadQueue importResult)
        {
            await _campaignReportExportUnitOfWork.CampaignReportExportRepository.UpdateAsync(importResult);
            await _campaignReportExportUnitOfWork.SaveChangesAsync();
        }
    }
}
