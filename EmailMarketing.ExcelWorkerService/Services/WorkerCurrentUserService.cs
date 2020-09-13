using System.Security.Claims;
using System;
using EmailMarketing.Common.Services;

namespace EmailMarketing.ExcelWorkerService.Services
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
