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
        Task<(IList<(int Value, string CampaignName, string Email, bool IsDelivered, bool IsSeen, DateTime SendDateTime, DateTime? SeenDateTime)> items, int Total, int TotalFilter)> GetAllCampaignReportAsync(
           string searchText,
           string orderBy,
           int pageIndex,
           int pageSize);

    }  
}
