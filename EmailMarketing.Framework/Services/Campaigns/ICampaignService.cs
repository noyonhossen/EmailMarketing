using EmailMarketing.Framework.Entities.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Campaigns
{
    public interface ICampaignService : IDisposable
    {
        Task<IList<(int Value, string Text, int Count)>> GetAllGroupsAsync(Guid? userId);
        Task<IList<EmailTemplate>> GetEmailTemplateByUserIdAsync(Guid? userId);
        Task AddCampaign(Campaign campaign);
        Task<IList<Campaign>> GetAllProcessingCampaign();
    }
}
