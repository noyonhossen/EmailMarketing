using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.UnitOfWorks.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Campaigns
{
    public class CampaignService : ICampaignService
    {
        private ICampaignUnitOfWork _campaignUnitOfWork;
        public CampaignService(ICampaignUnitOfWork campaignUnitOfWork)
        {
            _campaignUnitOfWork = campaignUnitOfWork;
        }
        public async Task<IList<(int Value, string CampaignName, string Email, bool IsDelivered, bool IsSeen, DateTime SendDateTime, DateTime? SeenDateTime)>> GetAllCampaignReportAsync(Guid? userId)
        {
            var result =  (await _campaignUnitOfWork.CampaingRepository.GetAsync(x => new { Value = x.Id, CampaignName = x.Campaign.Name, Email = x.Contact.Email, IsDelivered = x.IsDelivered, IsSeen = x.IsSeen, SendDateTime = x.SendDateTime, SeenDateTime = x.SeenDateTime},
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.Campaign.UserId == userId.Value), x => x.OrderBy(o => o.Contact.UserId) , null, true))
                                                   .Select(x => (Value: x.Value, CampaignName: x.CampaignName, Email: x.Email, IsDelivered: x.IsDelivered, IsSeen: x.IsSeen, SendDateTime: x.SendDateTime, SeenDateTime : x.SeenDateTime)).ToList();
            if (result == null) throw new NotFoundException(nameof(CampaignReport), userId);
            return result;
        }
        public void Dispose()
        {
            _campaignUnitOfWork?.Dispose();
        }
    }
}
