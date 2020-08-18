using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Repositories.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public class CampaignReportExportUnitOfWork : UnitOfWork, ICampaignReportExportUnitOfWork
    {
        public ICampaignReportExportRepository CampaignReportExportRepository { get; set; }

        public CampaignReportExportUnitOfWork(FrameworkContext dbContext,
            ICampaignReportExportRepository contactExportRepository) : base(dbContext)
        {
            this.CampaignReportExportRepository = contactExportRepository;
        }
    }
}
