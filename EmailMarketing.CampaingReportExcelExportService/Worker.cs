using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EmailMarketing.Common.Services;
using EmailMarketing.ExcelWorkerService.Templates;
using EmailMarketing.Framework.Enums;
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
                        if (Directory.Exists(item.FileUrl) == false)
                        {
                            DirectoryInfo directory = Directory.CreateDirectory(item.FileUrl);
                        }

                        var importResult = await _campaignReportExportService.GetDownloadQueueByIdAsync(item.Id);

                        if (item.DownloadQueueFor == DownloadQueueFor.CampaignAllReportExport)
                        {
                            await _campaignReportExportService.ExcelExportForAllCampaignAsync(item);
                        }
                        else if (item.DownloadQueueFor == DownloadQueueFor.CampaignDetailsReportExport)
                        {
                            await _campaignReportExportService.ExcelExportForCampaignWiseAsync(item);
                        }
                        importResult.IsProcessing = false;
                        importResult.IsSucceed = true;
                        await _campaignReportExportService.UpdateDownloadQueueAync(importResult);
                    }
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
