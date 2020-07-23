using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Entities
{
    public class CampaignReport : IAuditableEntity<int>
    {
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public Guid SMTPConfigId { get; set; }
        public SMTPConfig SMTPConfig { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsSeen { get; set; }
        public bool IsPersonalized { get; set; }
        public DateTime SeenDate { get; set; }
    }
}
