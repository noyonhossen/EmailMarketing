using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.ExcelWorkerService.Templates
{
    public partial class FileUploadFailedEmailTemplate
    {
        public string Name { get; set; }

        public FileUploadFailedEmailTemplate(string name)
        {
            Name = name;
        }
    }
}
