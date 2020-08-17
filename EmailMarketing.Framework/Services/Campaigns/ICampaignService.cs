using EmailMarketing.Framework.Entities.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Campaigns
{
    public interface ICampaignService : IDisposable
    {
        Task<IList<(int Value, string CampaignName, string Email, bool IsDelivered, bool IsSeen, DateTime SendDateTime, DateTime? SeenDateTime)>> GetAllCampaignReportAsync(
            Guid? userId);
    }
}
