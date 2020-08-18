using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public interface ICampaignReportUnitOfWork : IUnitOfWork
    {
        public ICampaingReportRepository CampaingReportRepository { get; set; }
    }
}
