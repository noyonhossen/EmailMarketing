using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Repositories.Campaigns;
using EmailMarketing.Framework.Repositories.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public class CampaignReportExportUnitOfWork : UnitOfWork, ICampaignReportExportUnitOfWork
    {
        public IDownloadQueueRepository DownloadQueueRepository { get; set; }
        public IDownloadQueueSubEntityRepository DownloadQueueSubEntityRepository { get; set; }
        public CampaignReportExportUnitOfWork(FrameworkContext dbContext,
            IDownloadQueueRepository downloadQueueRepository,
            IDownloadQueueSubEntityRepository downloadQueueSubEntityRepository) : base(dbContext)
        {
            this.DownloadQueueRepository = downloadQueueRepository;
            this.DownloadQueueSubEntityRepository = downloadQueueSubEntityRepository;
        }
    }
}
