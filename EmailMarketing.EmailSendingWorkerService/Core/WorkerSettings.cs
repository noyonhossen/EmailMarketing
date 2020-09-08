using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.EmailSendingWorkerService.Core
{
    public class WorkerSettings
    {
        public string EmailOpenTrackingUrl { get; set; }
        public string EmailSenderFileUrl { get; set; }
        public string EmailSenderLogUrl { get; set; }
        public string CompanyFullName { get; set; }
        public string CompanyShortName { get; set; }
        public string CompanyWebsiteUrl { get; set; }
        public string EncryptionDecryptionKey { get; set; }
    }
}
