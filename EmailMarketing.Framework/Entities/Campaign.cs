using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Entities
{
    public class Campaign : IAuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public DateTime SendDate { get; set; }
        public Guid UserId { get; set; }

        public IList<Group> Groups { get; set; }
        public IList<CampaignReport> CampaignReports { get; set; }

        public Campaign()
        {
            this.Groups = new List<Group>();
            this.CampaignReports = new List<CampaignReport>();
        }
    }
}
