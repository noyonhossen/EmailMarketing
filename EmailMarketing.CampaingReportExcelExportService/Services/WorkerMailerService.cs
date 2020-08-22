using EmailMarketing.CampaingReportExcelExportService.Entities;
using EmailMarketing.Common.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.CampaingReportExcelExportService.Services
{
    public class WorkerMailerService : IMailerService
    {
        private readonly WorkerSmtpSettings _workerSmtpSettings;

        public WorkerMailerService(IOptions<WorkerSmtpSettings> smtpSettings)
        {
            _workerSmtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var messgae = new MimeMessage();
                messgae.From.Add(new MailboxAddress(_workerSmtpSettings.SenderName, _workerSmtpSettings.SenderEmail));
                messgae.To.Add(MailboxAddress.Parse(email));
                messgae.Subject = subject;
                messgae.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_workerSmtpSettings.Server, _workerSmtpSettings.Port, true);

                    await client.AuthenticateAsync(_workerSmtpSettings.UserName, _workerSmtpSettings.Password);
                    await client.SendAsync(messgae);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
