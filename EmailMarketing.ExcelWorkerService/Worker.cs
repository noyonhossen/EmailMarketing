using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmailMarketing.Common.Services;
using EmailMarketing.ExcelWorkerService.Templates;
using EmailMarketing.Framework.Services.Contacts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.ExcelWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMailerService _mailerService;
        private readonly IContactUploadService _contactUploadService;

        public Worker(ILogger<Worker> logger, IContactUploadService contactUploadService, IMailerService mailerService)
        {
            _logger = logger;
            _mailerService = mailerService;
            _contactUploadService = contactUploadService;
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
                    var result = await _contactUploadService.GetUploadedContact();

                    foreach (var item in result)
                    {
                        _logger.LogInformation($"item values - file url is = {item.FileUrl}");
                        var importResult = await _contactUploadService.ContactExcelImportAsync(item.Id);

                        if(item.IsSendEmailNotify)
                        {
                            if(importResult.SucceedCount > 0)
                            {
                                var fileUploadConfirmationEmailTemplate = new FileUploadConfirmationEmailTemplate("Sir", importResult.SucceedCount, importResult.ExistCount, importResult.InvalidCount);
                                var emailBody = fileUploadConfirmationEmailTemplate.TransformText();

                                await _mailerService.SendEmailAsync(item.SendEmailAddress, "File Upload Confirmation", emailBody);
                            }
                            else
                            {
                                var fileUploadFailedEmailTemplate = new FileUploadFailedEmailTemplate("Sir");
                                var emailBody = fileUploadFailedEmailTemplate.TransformText();
                                await _mailerService.SendEmailAsync(item.SendEmailAddress, "Upload Failed", emailBody);
                            }
                        }
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
