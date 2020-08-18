using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Repositories.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public interface ICampaignReportExportUnitOfWork : IUnitOfWork
    {
        ICampaignReportExportRepository CampaignReportExportRepository { get; set; }
    }
}
