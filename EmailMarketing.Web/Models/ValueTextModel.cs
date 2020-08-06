using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Models
{
    public class ValueTextModel
    {
        public object Value { get; set; }
        public string Text { get; set; }
        public bool IsStandard { get; set; }
    }
}
