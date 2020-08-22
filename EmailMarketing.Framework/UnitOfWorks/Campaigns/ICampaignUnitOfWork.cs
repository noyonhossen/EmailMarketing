using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailMarketing.Framework.Repositories.Campaign;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public interface ICampaignUnitOfWork : IUnitOfWork
    {
        ICampaignReportRepository CampaignReportRepository { get; set; }
        ICampaignRepository CampaignRepository { get; set; }
    }
}
