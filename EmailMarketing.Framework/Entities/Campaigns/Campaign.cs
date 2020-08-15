using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Entities.Campaigns
{
    public class Campaign : IAuditableEntity<int>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public DateTime SendDateTime { get; set; }
        public bool IsDraft { get; set; }
        public bool IsProcessing { get; set; }
        public bool IsSucceed { get; set; }

        public IList<CampaignGroup> CampaignGroups { get; set; }
        public IList<CampaignReport> CampaignReports { get; set; }

        public Campaign()
        {
            this.CampaignGroups = new List<CampaignGroup>();
            this.CampaignReports = new List<CampaignReport>();
        }
    }
}
