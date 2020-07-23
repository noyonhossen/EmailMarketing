using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Entities
{
    public class CampaignGroup
    {
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
