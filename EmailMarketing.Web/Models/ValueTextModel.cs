using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Models
{
    public class ValueTextModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
        public int Count { get; set; }
    }
}
