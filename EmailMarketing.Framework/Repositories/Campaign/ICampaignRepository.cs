using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities.Campaigns;
namespace EmailMarketing.Framework.Repositories.Campaign
{
    public interface ICampaignRepository : IRepository<EmailMarketing.Framework.Entities.Campaigns.Campaign, int, FrameworkContext>
    {
    }
}
