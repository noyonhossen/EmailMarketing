using EmailMarketing.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.CampaingReportExcelExportService.Services
{
    public class WorkerDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
