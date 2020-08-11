using System;
using EmailMarketing.Common.Services;

namespace EmailMarketing.EmailSendingWorkerService.Services
{
    public class WorkerDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
