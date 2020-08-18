using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.ExcelWorkerService.Templates
{
    public partial class FileUploadConfirmationEmailTemplate
    {
        public string Name { get; set; }
        public int SuccessCount { get; set; }
        public int ExistsCount { get; set; }
        public int InvalidCount { get; set; }

        public FileUploadConfirmationEmailTemplate(string name, int successCount, int existsCount, int invalidCount)
        {
            Name = name;
            SuccessCount = successCount;
            ExistsCount = existsCount;
            InvalidCount = invalidCount;
        }
    }
}
