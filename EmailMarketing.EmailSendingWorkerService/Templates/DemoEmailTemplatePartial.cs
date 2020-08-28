using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.EmailSendingWorkerService.Templates
{
    public partial class DemoEmailTemplate
    {
        public string Name { get; private set; }
        public int TotalCount { get; set; }
        public int TotalFailed { get; set; }

        public DemoEmailTemplate(string name, int totalCount, int totalFailed)
        {
            this.Name = name;
            this.TotalCount = totalCount;
            this.TotalFailed = totalFailed;
        }
    }
}
