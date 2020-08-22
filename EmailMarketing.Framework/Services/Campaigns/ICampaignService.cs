using EmailMarketing.Framework.Entities.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace EmailMarketing.Framework.Services.Campaigns
{
    public interface ICampaignService
    {
        Task<(IList<Campaign> Items, int Total, int TotalFilter)> GetAllCampaignAsync(
        Guid? userId,
        string searchText,
        string orderBy,
        int pageIndex,
        int pageSize);
        Task<(IList<CampaignReport> Items, int Total, int TotalFilter)> GetAllCampaignReportAsync(
        Guid? userId,
        int campaignId,
        string searchText,
        string orderBy,
        int pageIndex,
        int pageSize);
        
    }  
}
