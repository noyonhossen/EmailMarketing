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
    public class CreateCampaignModel : CampaignsBaseModel
    {
        public Guid UserId { get; set; }
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Email Subject")]
        public string EmailSubject { get; set; }
        [Required]
        [Display(Name = "Send DateTime")]
        public DateTime? SendDateTime { get; set; }
        [Display(Name = "Notify Me")]
        public bool IsSendEmailNotify { get; set; }
        [Display(Name = "Sender Email")]
        public string SendEmailAddress { get; set; }
        [Display(Name = "Draft")]
        public bool IsDraft { get; set; }
        public int? SelectedTemplateId { get; set; }
        [Display(Name = "Template Title")]
        public string EmailTemplateTitle { get; set; }
        [Display(Name = "Email Body")]
        public string EmailTemplateBody { get; set; }
        public IList<EmailTemplate> EmailTemplateList { get; set; }
        public IList<CampaignValueTextModel> GroupSelectList { get; set; }
        

        public CreateCampaignModel(ICampaignService campaignService,
            ICurrentUserService currentUserService,
            IEmailTemplateService emailTemplateService)
            : base(campaignService, currentUserService, emailTemplateService)
        {

        }
        public CreateCampaignModel() : base()
        {

        }

        public async Task<IList<CampaignValueTextModel>> GetAllGroupForSelectAsync()
        {
            return (await _campaignService.GetAllGroupsAsync(_currentUserService.UserId))
                                           .Select(x => new CampaignValueTextModel { Value = x.Value, Text = x.Text, Count = x.Count, IsChecked = false }).ToList();
        }

        public async Task<IList<EmailTemplate>> GetTemplateByUserIDAsync()
        {
            return (await _campaignService.GetEmailTemplateByUserIdAsync(_currentUserService.UserId));
        }
    }
}
