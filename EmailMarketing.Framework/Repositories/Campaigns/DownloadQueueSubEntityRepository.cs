using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Campaigns
{
    public class DownloadQueueSubEntityRepository : Repository<DownloadQueueSubEntity, int, FrameworkContext>, IDownloadQueueSubEntityRepository
    {
        public DownloadQueueSubEntityRepository(FrameworkContext dbContext)
           : base(dbContext)
        {

        }
    }
}
