using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Campaigns
{
    public class CampaignValueTextModel
    {

        public string CampaignName { get; set; }
        public string Email { get; set; }
        public int Value { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsSeen { get; set; }
        public DateTime SendDateTime { get; set; }
        public DateTime? SeenDateTime { get; set; }

        public int Value { get; set; }
        public string Text { get; set; }
        public int Count { get; set; }

        public bool IsChecked { get; set; }
    }
}
