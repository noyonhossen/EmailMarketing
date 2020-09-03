using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.CampaingReportExcelExportService.Templates
{
    public partial class CampReportExportTemplate
    {
        public string Name { get; set; }

        public CampReportExportTemplate(string name)
        {
            this.Name = name;
        }
    }
}
