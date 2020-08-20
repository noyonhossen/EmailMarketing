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
        private ICampaignUnitOfWork _campaignUnitOfWork;
        public CampaignReportExportService(ICampaignReportExportUnitOfWork campaignReportExportUnitOfWork, ICampaignReportUnitOfWork campaignReportUnitOfWork)
        {
            _campaignReportExportUnitOfWork = campaignReportExportUnitOfWork;
            _campaignReportUnitOfWork = campaignReportUnitOfWork;
        }
        //public async Task<IList<(int Value, string Text, int Count)>> GetAllCampaignsAsync(Guid? userId)
        //{
        //    return (await _campaignUnitOfWork.CampaignRepository.GetAsync(x => new { Value = x.Id, Text = x.Name, Count = x.CampaignGroups.Count() },
        //                                           x => !x.IsDeleted && x.IsActive &&
        //                                           (!userId.HasValue || x.UserId == userId.Value), x => x.OrderBy(o => o.Name), null, true))
        //                                           .Select(x => (Value: x.Value, Text: x.Text, Count: x.Count)).ToList();
        //}
        //public async Task<IList<Campaign>> GetAllCampaignsAsync(Guid? userId)
        //{
        //    var campaigns = await _campaignUnitOfWork.CampaignRepository.GetAsync<Campaign>(
        //        x => x, x => (!userId.HasValue || x.UserId == userId.Value), null, x => x.Include(i => i.ContactValueMaps).ThenInclude(i => i.FieldMap)
        //        .Include(i => i.ContactGroups).ThenInclude(i => i.Group), true
        //        );
        //    return campaigns;
        //}
        //public async Task<IList<Campaign>> GetAllGroupsByIdAsync(int campaignId)
        //{
        //    var campaigns = await _campaignReportUnitOfWork.CampaingReportRepository.GetAsync<Campaign>(
        //        x => x, x => (x.Id == campaignId), null, null, true
        //        );
        //    return campaigns;
        //}
        //public async Task<Campaign> GetCampaignReportById(int campaignId)
        //{
        //    var contact = await _campaignReportUnitOfWork.CampaingReportRepository.GetByIdAsync(campaignId);
        //    return contact;
        //}
        public async Task<IList<DownloadQueue>> GetDownloadQueue()
        {
            var result = await _campaignReportExportUnitOfWork.DownloadQueueRepository.GetAsync(x => x, x => x.IsProcessing == true && x.IsSucceed == false, null, x=> x.Include(y=> y.DownloadQueueSubEntities), true);
            return result;
        }

        public async Task<DownloadQueue> GetDownloadQueueByIdAsync(int campaingReportId)
        {
            var contactUpload = await _campaignReportExportUnitOfWork.DownloadQueueRepository.GetFirstOrDefaultAsync(x => x, x => x.Id == campaingReportId,
                                    null, true);
            return contactUpload;
        }
        public async Task UpdateDownloadQueueAync(DownloadQueue downloadQueue)
        {
            await _campaignReportExportUnitOfWork.DownloadQueueRepository.UpdateAsync(downloadQueue);
            await _campaignReportExportUnitOfWork.SaveChangesAsync();
        }
        public void Dispose()
        {
            _campaignReportUnitOfWork?.Dispose();
        }
        public async Task<IList<(int Value, string CampaignName, string Email, bool IsDelivered, bool IsSeen, DateTime SendDateTime, DateTime? SeenDateTime)>> GetAllCampaignReportAsync(Guid? userId)
        {
            var result = (await _campaignReportUnitOfWork.CampaingReportRepository.GetAsync(x => new { Value = x.Id, CampaignName = x.Campaign.Name, Email = x.Contact.Email, IsDelivered = x.IsDelivered, IsSeen = x.IsSeen, SendDateTime = x.SendDateTime, SeenDateTime = x.SeenDateTime },
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.Campaign.UserId == userId.Value), x => x.OrderBy(o => o.Contact.Email), null, true))
                                                   .Select(x => (Value: x.Value, CampaignName: x.CampaignName, Email: x.Email, IsDelivered: x.IsDelivered, IsSeen: x.IsSeen, SendDateTime: x.SendDateTime, SeenDateTime: x.SeenDateTime)).ToList();
            if (result == null) throw new NotFoundException(nameof(CampaignReport), userId);
            return result;
        }
        public async Task<IList<(int Value, string CampaignName, string Email, bool IsDelivered, bool IsSeen, DateTime SendDateTime, DateTime? SeenDateTime)>> GetCampaignWiseReportAsync(Guid? userId,int campaignId)
        {
            var result = (await _campaignReportUnitOfWork.CampaingReportRepository.GetAsync(x => new { Value = x.Id, CampaignName = x.Campaign.Name, Email = x.Contact.Email, IsDelivered = x.IsDelivered, IsSeen = x.IsSeen, SendDateTime = x.SendDateTime, SeenDateTime = x.SeenDateTime },
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.Campaign.UserId == userId.Value) && (x.CampaignId == campaignId), x => x.OrderBy(o => o.Contact.Email), null, true))
                                                   .Select(x => (Value: x.Value, CampaignName: x.CampaignName, Email: x.Email, IsDelivered: x.IsDelivered, IsSeen: x.IsSeen, SendDateTime: x.SendDateTime, SeenDateTime: x.SeenDateTime)).ToList();
            if (result == null) throw new NotFoundException(nameof(CampaignReport), userId);
            return result;
        }
        public async Task SaveDownloadQueueAsync(DownloadQueue downloadQueue)
        {
            await _campaignReportExportUnitOfWork.DownloadQueueRepository.AddAsync(downloadQueue);
            await _campaignReportExportUnitOfWork.SaveChangesAsync();
        }
        public async Task AddDownloadQueueSubEntities(DownloadQueueSubEntity downloadQueueSubEntity)
        {
            await _campaignReportExportUnitOfWork.DownloadQueueSubEntityRepository.AddAsync(downloadQueueSubEntity);
            await _campaignReportExportUnitOfWork.SaveChangesAsync();
        }

        Task<IList<CampaignGroup>> ICampaignReportExportService.GetAllGroupsByIdAsync(int campaignId)
        {
            throw new NotImplementedException();
        }

        Task<CampaignReport> ICampaignReportExportService.GetCampaignReportById(int campaignId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Campaign>> GetAllCampaignsAsync(Guid? userId)
        {
            throw new NotImplementedException();
        }
    }
}
