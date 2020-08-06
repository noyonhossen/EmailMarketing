using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Services.Smtp;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Smtp
{
    public class EditSMTPModel:SMTPBaseModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Server { get; set; }
        [Required]
        public int Port { get; set; }
        [Required]
        public string SenderName { get; set; }
        [Required]
        public string SenderEmail { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool EnableSSL { get; set; }

        public EditSMTPModel(ISmtpService smtpService, IApplicationUserService applicationUserService,
           ICurrentUserService currentUserService) : base(smtpService, applicationUserService, currentUserService)
        {

        }

        public EditSMTPModel() : base()
        {

        }

        public async Task LoadByIdAsync(Guid id)
        {
            var result = await _smtpService.GetByIdAsync(id);
            this.Id = result.Id;
            this.Server = result.Server;
            this.Port = result.Port;
            this.SenderName = result.SenderName;
            this.SenderEmail = result.SenderEmail;
            this.UserName = result.UserName;
            this.Password = result.Password;
            this.EnableSSL = result.EnableSSL;
        }

        public async Task UpdateAsync()
        {
            var entity = new SMTPConfig
            {
                Id = this.Id,
                Server = this.Server,
                Port = this.Port,
                SenderName = this.SenderName,
                SenderEmail = this.SenderEmail,
                UserName = this.UserName,
                Password = this.Password,
                EnableSSL = this.EnableSSL
            };
            await _smtpService.UpdateAsync(entity);
        }
    }
}
