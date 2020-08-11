using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmailMarketing.Common.Services;
using EmailMarketing.EmailSendingWorkerService.Templates;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.EmailSendingWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMailerService _mailerService;

        public Worker(ILogger<Worker> logger, IMailerService mailerService)
        {
            _logger = logger;
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

                var list = new List<string>();
                list.Add("shamimalmamunsamir@gmail.com");
                list.Add("bad5432@gmail.com");
                list.Add("shamimalmamunsamir@gmail.com");
                list.Add("bad5432@gmail.com");
                list.Add("shamimalmamunsamir@gmail.com");
                list.Add("samamun009@gmail.com");
                list.Add("bad5432@gmail.com");

                var demoEmailTempalte = new DemoEmailTemplate();
                var emailBody = demoEmailTempalte.TransformText();

                try
                {
                    foreach(var item in list)
                    {
                        await _mailerService.SendEmailAsync(item, "Bulk Mail", emailBody);
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogInformation($"Error Message: {ex.Message}");
                }

                await Task.Delay(20000, stoppingToken);
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
