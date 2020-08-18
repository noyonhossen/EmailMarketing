using EmailMarketing.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.CampaingReportExcelExportService.Services
{
    public class WorkerCurrentUserService : ICurrentUserService
    {
        public WorkerCurrentUserService()
        {
            UserId = Guid.Empty;
            IsAuthenticated = false;
        }

        public Guid UserId { get; }
        public bool IsAuthenticated { get; }
    }
}
