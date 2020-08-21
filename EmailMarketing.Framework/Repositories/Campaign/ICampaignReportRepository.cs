using System;
using System.Collections.Generic;
using System.Text;
using EmailMarketing.Data;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.Context;

namespace EmailMarketing.Framework.Repositories.Campaign
{
    public interface ICampaignReportRepository : IRepository<CampaignReport, int,FrameworkContext>
    {

    }
    
}
