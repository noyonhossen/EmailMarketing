using EmailMarketing.Framework.Entities.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Campaigns
{
    public interface ICampaignService : IDisposable
    {
        Task<IList<CampaignReport>> GetAllCampaignReportAsync(
            Guid? userId);
    }
}
