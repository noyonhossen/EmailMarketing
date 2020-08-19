using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.UnitOfWorks.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Campaigns
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private ICampaignUnitOfWork _campaignUnitOfWork;

        public EmailTemplateService(ICampaignUnitOfWork campaignUnitOfWork)
        {
            _campaignUnitOfWork = campaignUnitOfWork;
        }

        public async Task AddEmailTemplateAsync(EmailTemplate emailTemplate)
        {
            var count = await _campaignUnitOfWork.EmailTemplateRepository.GetCountAsync(x => x.EmailTemplateBody == emailTemplate.EmailTemplateBody);
            if (count > 0)
            {
                new DuplicationException("Template Already Exists");
            }

            await _campaignUnitOfWork.EmailTemplateRepository.AddAsync(emailTemplate);
            await _campaignUnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            _campaignUnitOfWork?.Dispose();
        }
    }
}
