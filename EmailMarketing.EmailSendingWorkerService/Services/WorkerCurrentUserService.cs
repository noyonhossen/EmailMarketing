using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;
using EmailMarketing.Common.Services;

namespace EmailMarketing.EmailSendingWorkerService.Services
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
