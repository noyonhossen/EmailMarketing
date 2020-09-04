using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.ExcelExportWorkerService.Core
{
    public class WorkerSettings
    {
        public string ContactExportFileUrl { get; set; }
        public string ContactExportLogUrl { get; set; }
        public string CompanyFullName { get; set; }
        public string CompanyShortName { get; set; }
        public string CompanyWebsiteUrl { get; set; }
    }
}
