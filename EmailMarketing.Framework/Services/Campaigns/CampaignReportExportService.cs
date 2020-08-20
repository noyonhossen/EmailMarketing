using ClosedXML.Excel;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.UnitOfWorks.Campaigns;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task ExcelExportForAllCampaignAsync(DownloadQueue downloadQueue)
        {
            using (var workbook = new XLWorkbook())
            {
                var campaignReport = await GetAllCampaignReportAsync(downloadQueue.UserId);

                var worksheet = workbook.Worksheets.Add("All Campaign Report");
                var currentRow = 1;
                int i = 3;

                worksheet.Cell(currentRow, 1).Value = "Email";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 2).Value = "Delivered";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 3).Value = "Seen";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 4).Value = "Send Date";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 5).Value = "Seen Date";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

                foreach (var report in campaignReport)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = report.Email;
                    worksheet.Cell(currentRow, 2).Value = report.IsDelivered == true ? "Yes" : "No";
                    worksheet.Cell(currentRow, 3).Value = report.IsSeen == true ? "Yes" : "No";
                    worksheet.Cell(currentRow, 4).Value = "" + report.SendDateTime.ToString();
                    worksheet.Cell(currentRow, 5).Value = report.SeenDateTime.ToString();
                }
                worksheet.Columns("1", "5").AdjustToContents();

                var memory = new MemoryStream();
                using (var stream = new FileStream(Path.Combine(downloadQueue.FileUrl, downloadQueue.FileName), FileMode.Append))
                {
                    workbook.SaveAs(stream);
                }
            }
        }
        public async Task ExcelExportForCampaignWiseAsync(DownloadQueue downloadQueue)
        {
            for (int cnt = 0; cnt < downloadQueue.DownloadQueueSubEntities.Count(); cnt++)
            {
                using (var workbook = new XLWorkbook())
                {
                    var campaignReport = await GetCampaignWiseReportAsync(downloadQueue.UserId, downloadQueue.DownloadQueueSubEntities[cnt].DownloadQueueSubEntityId);

                    var worksheet = workbook.Worksheets.Add("CampaignWiseReport");
                    var currentRow = 1;
                    int i = 3;

                    worksheet.Cell(currentRow, 1).Value = "Email";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 2).Value = "Delivered";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 3).Value = "Seen";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 4).Value = "Send Date";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 5).Value = "Seen Date";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

                    foreach (var report in campaignReport)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = report.Email;
                        worksheet.Cell(currentRow, 2).Value = report.IsDelivered == true ? "Yes" : "No";
                        worksheet.Cell(currentRow, 3).Value = report.IsSeen == true ? "Yes" : "No";
                        worksheet.Cell(currentRow, 4).Value = "" + report.SendDateTime.ToString();
                        worksheet.Cell(currentRow, 5).Value = report.SeenDateTime.ToString();
                    }
                    worksheet.Columns("1", "5").AdjustToContents();

                    var memory = new MemoryStream();
                    using (var stream = new FileStream(Path.Combine(downloadQueue.FileUrl, downloadQueue.FileName), FileMode.Append))
                    {
                        workbook.SaveAs(stream);
                    }
                }
            }
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
    }
}
