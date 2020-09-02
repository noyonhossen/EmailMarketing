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
using EmailMarketing.EmailSendingWorkerService.Entities;
using EmailMarketing.Framework.Entities.SMTP;
using EmailMarketing.Common.Extensions;
using EmailMarketing.Common.Constants;

namespace EmailMarketing.EmailSendingWorkerService.Services
{
    public class WorkerMailerService : IWorkerMailerService, IMailerService
    {
        private readonly WorkerSmtpSettings _workerSmtpSettings;

        public WorkerMailerService(IOptions<WorkerSmtpSettings> smtpSettings)
        {
            _workerSmtpSettings = smtpSettings.Value;
        }

        public async Task<bool> SendBulkEmailAsync(string email, string subject, string body, SMTPConfig sMTPConfig)
        {
            try
            {
                var messgae = new MimeMessage();
                messgae.From.Add(new MailboxAddress(sMTPConfig.SenderName, sMTPConfig.SenderEmail));
                messgae.To.Add(MailboxAddress.Parse(email));
                messgae.Subject = subject;
                messgae.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(sMTPConfig.Server, sMTPConfig.Port, true);

                    await client.AuthenticateAsync(sMTPConfig.UserName, sMTPConfig.Password.ToDecryptString(ConstantsValue.EncryptDecryptKey));
                    await client.SendAsync(messgae);
                    await client.DisconnectAsync(true);
                    return true;
                }
            }
            catch (Exception e)
            {
                //throw new InvalidOperationException(e.Message);
                return false;
            }
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var messgae = new MimeMessage();
                messgae.From.Add(new MailboxAddress(_workerSmtpSettings.SenderName, _workerSmtpSettings.SenderEmail));
                InternetAddressList toList = new InternetAddressList();
                foreach (var item in email.Split(',').ToList())
                {
                    toList.Add(MailboxAddress.Parse(item.Trim()));
                }
                //messgae.To.Add(MailboxAddress.Parse(email));
                messgae.To.AddRange(toList);
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
