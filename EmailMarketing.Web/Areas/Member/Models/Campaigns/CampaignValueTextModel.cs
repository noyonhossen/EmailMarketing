using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Campaigns
{
    public class CampaignValueTextModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public int Count { get; set; }

        public bool IsChecked { get; set; }
    }
}
