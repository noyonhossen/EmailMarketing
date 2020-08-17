using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.UnitOfWorks.Campaigns;
using EmailMarketing.Framework.UnitOfWorks.Groups;
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
        private IGroupUnitOfWork _groupUnitOfWork;

        public CampaignService(ICampaignUnitOfWork campaignUnitOfWork, IGroupUnitOfWork groupUnitOfWork)
        {
            _campaignUnitOfWork = campaignUnitOfWork;
            _groupUnitOfWork = groupUnitOfWork;
        }

        public async Task<IList<(int Value, string Text, int Count)>> GetAllGroupsAsync(Guid? userId)
        {
            return (await _groupUnitOfWork.GroupRepository.GetAsync(x => new { Value = x.Id, Text = x.Name, Count = x.ContactGroups.Count() },
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.UserId == userId.Value), x => x.OrderBy(o => o.Name), null, true))
                                                   .Select(x => (Value: x.Value, Text: x.Text, Count: x.Count)).ToList();
        }

        public void Dispose()
        {
            _campaignUnitOfWork?.Dispose();
        }
    }
}
