using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.UnitOfWorks.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Campaigns
{
    public class CampaignReportService : ICampaignReportService
    {
        private readonly ICampaignReportUnitOfWork _campaignReportUnitOfWork;

        public CampaignReportService(ICampaignReportUnitOfWork campaignReportUnitOfWork)
        {
            _campaignReportUnitOfWork = campaignReportUnitOfWork;
        }

        public async Task AddCampaingReportAsync(IList<CampaignReport> campaignReports)
        {
            await _campaignReportUnitOfWork.CampaingReportRepository.AddRangeAsync(campaignReports);
            await _campaignReportUnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            _campaignReportUnitOfWork?.Dispose();
        }
    }
}
