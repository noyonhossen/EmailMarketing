using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.ExcelWorkerService.Core
{
    public class WorkerSettings
    {
        public string ContactImportFileUrl { get; set; }
        public string ContactImportLogUrl { get; set; }
        public string CompanyFullName { get; set; }
        public string CompanyShortName { get; set; }
        public string CompanyWebsiteUrl { get; set; }
    }
}
