using EmailMarketing.Framework.Context;
using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Campaign;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public class CampaignUnitOfWork : UnitOfWork, ICampaignUnitOfWork
    {
        
        public ICampaignReportRepository CampaignReportRepository { get; set; }
        public ICampaignRepository CampaignRepository { get; set; }

        public CampaignUnitOfWork(FrameworkContext dbContext , ICampaignReportRepository campaignReportRepository, ICampaignRepository campaignRepository) : base(dbContext)
        {
            CampaignReportRepository = campaignReportRepository;
            CampaignRepository = campaignRepository;
        }

        
    }
}