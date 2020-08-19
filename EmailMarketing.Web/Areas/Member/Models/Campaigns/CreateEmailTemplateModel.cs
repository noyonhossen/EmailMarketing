using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.Services.Campaigns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Campaigns
{
    public class CreateEmailTemplateModel : CampaignsBaseModel
    {
        public Guid UserId { get; set; }

        [Display(Name = "Title")]
        public string EmailTemplateName { get; set; }

        [Required]
        [Display(Name = "Email Body")]
        public string EmailTemplateBody { get; set; }

        public CreateEmailTemplateModel(ICampaignService campaignService, 
            ICurrentUserService currentUserService,
            IEmailTemplateService emailTemplateService) 
            : base(campaignService, currentUserService, emailTemplateService)
        {

        }
        public CreateEmailTemplateModel() : base()
        {

        }

        public async Task CreateEmailTemplate()
        {
            var emailTempalte = new EmailTemplate
            {
                UserId = _currentUserService.UserId,
                EmailTemplateName = this.EmailTemplateName,
                EmailTemplateBody = this.EmailTemplateBody
            };

            await _emailTemplateService.AddEmailTemplateAsync(emailTempalte);
        }
    }
}
