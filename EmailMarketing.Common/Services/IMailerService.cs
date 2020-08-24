using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Common.Services
{
    public interface IMailerService
    {
        Task SendEmailAsync(string email, string subject, string body);
        
    }
}
