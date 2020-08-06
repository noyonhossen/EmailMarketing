using System;
using EmailMarketing.Common.Services;

namespace EmailMarketing.ExcelWorkerService.Services
{
    public class WorkerDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
