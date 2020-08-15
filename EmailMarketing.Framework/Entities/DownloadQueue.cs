using EmailMarketing.Data;
using EmailMarketing.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Entities
{
    public class DownloadQueue : IAuditableEntity<int>
    {
        public DownloadFor DownloadFor { get; set; }
        public int DownloadEntityId { get; set; }
        public Guid UserId { get; set; }
        public bool IsSendEmailNotify { get; set; }
        public string SendEmailAddress { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public bool IsSucceed { get; set; }
        public int SucceedEntryCount { get; set; }
        public bool IsProcessing { get; set; }
    }
}
