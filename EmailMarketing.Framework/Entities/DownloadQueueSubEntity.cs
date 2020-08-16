using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Entities
{
    public class DownloadQueueSubEntity : IEntity<int>
    {
        public int DownloadQueueId { get; set; }
        public DownloadQueue DownloadQueue { get; set; }
        public int DownloadQueueSubEntityId { get; set; }
    }
}
