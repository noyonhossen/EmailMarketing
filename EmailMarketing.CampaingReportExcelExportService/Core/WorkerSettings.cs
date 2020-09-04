using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.CampaingReportExcelExportService.Core
{
    public class WorkerSettings
    {
        public string CampaignExportFileUrl { get; set; }
        public string CampaignExportLogUrl { get; set; }
        public string CompanyFullName { get; set; }
        public string CompanyShortName { get; set; }
        public string CompanyWebsiteUrl { get; set; }
    }
}
