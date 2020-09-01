using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.CustomUI;
using EmailMarketing.Common.Services;
using EmailMarketing.ExcelExportWorkerService.Templates;
using EmailMarketing.Framework.Enums;
using EmailMarketing.Framework.Services.Contacts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.ExcelExportWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IContactExportService _contactExportService;
        private readonly IMailerService _mailerService;

        public Worker(ILogger<Worker> logger, IContactExportService contactExportService, IMailerService mailerService)
        {
            _logger = logger;
            _contactExportService = contactExportService;
            _mailerService = mailerService;
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
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    var result = await _contactExportService.GetDownloadQueueAsync();

                    foreach (var item in result)
                    {
                        //To create directory if not exist
                        if (Directory.Exists(item.FileUrl) == false)
                        {
                            DirectoryInfo directory = Directory.CreateDirectory(item.FileUrl);
                        }
                        
                        var importResult = await _contactExportService.GetDownloadQueueByIdAsync(item.Id);
                        if (item.DownloadQueueFor == DownloadQueueFor.ContactAllExport)
                        {
                            await _contactExportService.ExcelExportForAllContactsAsync(item);
                        }

                        else if (item.DownloadQueueFor == DownloadQueueFor.ContactGroupWiseExport)
                        {
                            await _contactExportService.ExcelExportForGroupwiseContactsAsync(item);
                        }

                        importResult.IsProcessing = false;
                        importResult.IsSucceed = true;
                        //importResult.FileUrl = Path.Combine(item.FileUrl, item.FileName);
                        await _contactExportService.UpdateDownloadQueueAsync(importResult);
                        
                        //Sending Email
                        if(item.IsSendEmailNotify)
                        {
                            var url = Path.Combine(item.FileName, item.FileName);
                            var emailSubject = "Contact Export Confirmation";
                            var excelExportConfirmationTemplate = new ExcelExportConfirmationTemplate("Shamim", url);
                            var emailBody = excelExportConfirmationTemplate.TransformText();

                            await _mailerService.SendEmailAsync(item.SendEmailAddress, emailSubject, emailBody);
                        }
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
