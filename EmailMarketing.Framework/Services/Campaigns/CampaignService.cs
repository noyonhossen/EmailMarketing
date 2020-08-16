using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.UnitOfWorks.Campaigns;
using System;
using System.Collections.Generic;
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
        //public async Task<IList<CampaignReport>> GetAllCampaignReportAsync(Guid? userId)
        //{
        //    var result = _campaignUnitOfWork.CampaingRepository.GetAsync();
        //    if (result == null) throw new NotFoundException(nameof(CampaignReport), userId);
        //    return result;
        //}
        public void Dispose()
        {
            _campaignUnitOfWork?.Dispose();
        }

        public Task<IList<CampaignReport>> GetAllCampaignReportAsync(Guid? userId)
        {
            throw new NotImplementedException();
        }
    }
}
