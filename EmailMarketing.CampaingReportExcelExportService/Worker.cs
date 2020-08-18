using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EmailMarketing.Common.Services;
using EmailMarketing.ExcelWorkerService.Templates;
using EmailMarketing.Framework.Services.Campaigns;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.CampaingReportExcelExportService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMailerService _mailerService;
        private readonly ICampaignReportExportService _campaignReportExportService;

        public Worker(ILogger<Worker> logger, ICampaignReportExportService campaignReportExportService, IMailerService mailerService)
        {
            _logger = logger;
            _mailerService = mailerService;
            _campaignReportExportService = campaignReportExportService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Worker started at: {DateTime.Now}");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker Service running at: {time}", DateTimeOffset.Now);

                try
                {
                    var result = await _campaignReportExportService.GetDownloadQueue();

                    foreach (var item in result)
                    {
                        _logger.LogInformation($"item values - file url is = {item.FileUrl}");
                        var importResult = await _campaignReportExportService.GetDownloadQueueByIdAsync(item.Id);

                        using (var workbook = new XLWorkbook())
                        {
                            var campaignReport = await _campaignReportExportService.GetAllCampaignReportAsync(item.UserId);

                            var worksheet = workbook.Worksheets.Add("CampaignReport");
                            var currentRow = 1;
                            int i = 3;

                            worksheet.Cell(currentRow, 1).Value = "Email";
                            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                            worksheet.Column(1).AdjustToContents();
                            worksheet.Cell(currentRow, 2).Value = "Delivered";
                            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                            worksheet.Column(2).AdjustToContents();
                            worksheet.Cell(currentRow, 3).Value = "Seen";
                            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                            worksheet.Column(3).AdjustToContents();
                            worksheet.Cell(currentRow, 4).Value = "Send Date";
                            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                            worksheet.Column(4).AdjustToContents();
                            worksheet.Cell(currentRow, 5).Value = "Seen Date";
                            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                            worksheet.Column(5).AdjustToContents();

                            foreach (var report in campaignReport)
                            {
                                currentRow++;
                                worksheet.Cell(currentRow, 1).Value = report.Email;
                                worksheet.Cell(currentRow, 2).Value = report.IsDelivered == true ? "Yes" : "NO";
                                worksheet.Cell(currentRow, 3).Value = report.IsSeen == true ? "Yes" : "NO";
                                worksheet.Cell(currentRow, 4).Value = "" + report.SendDateTime.ToString();
                                worksheet.Cell(currentRow, 5).Value = report.SeenDateTime.ToString();
                            }
                            //need to change
                            worksheet.Columns("1", "5").AdjustToContents();

                            var memory = new MemoryStream();
                            using (var stream = new FileStream(Path.Combine(item.FileUrl, item.FileName), FileMode.Append))
                            {
                                workbook.SaveAs(stream);
                                //await stream.CopyToAsync(memory);
                            }
                            //workbook.SaveAs("D:\\Working\\Demo.xlsx");


                        }
                        importResult.IsProcessing = false;
                        importResult.IsSucceed = true;
                        importResult.FileUrl = Path.Combine(item.FileUrl, item.FileName);
                        _campaignReportExportService.UpdateDownloadQueue(importResult);

                        //if (item.IsSendEmailNotify)
                        //{
                        //    if (importResult.SucceedEntryCount > 0)
                        //    {
                        //        var fileUploadConfirmationEmailTemplate = new FileUploadConfirmationEmailTemplate("Noyon", importResult.SucceedEntryCount);
                        //        var emailBody = fileUploadConfirmationEmailTemplate.TransformText();

                        //        await _mailerService.SendEmailAsync(item.SendEmailAddress, "File Upload Confirmation", emailBody);
                        //    }
                        //    else
                        //    {
                        //        var fileUploadFailedEmailTemplate = new FileUploadFailedEmailTemplate("Shamim");
                        //        var emailBody = fileUploadFailedEmailTemplate.TransformText();
                        //        await _mailerService.SendEmailAsync(item.SendEmailAddress, "Upload Failed", emailBody);
                        //    }
                        //}

                    }
                    _logger.LogInformation("item values is done showing");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error message : {ex.Message}");
                }

                await Task.Delay(120000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Worker stopped at: {DateTime.Now}");
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _logger.LogInformation($"Worker disposed at: {DateTime.Now}");
            base.Dispose();
        }
    }
}
