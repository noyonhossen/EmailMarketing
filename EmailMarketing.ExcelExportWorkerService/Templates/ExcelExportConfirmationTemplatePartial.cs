using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.ExcelExportWorkerService.Templates
{
    public partial class ExcelExportConfirmationTemplate
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        public ExcelExportConfirmationTemplate(string name, string url)
        {
            this.Name = name;
            this.Url = url;
        }
    }
}
