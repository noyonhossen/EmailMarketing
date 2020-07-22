using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Entities
{
    public class Contact : IAuditableEntity<int>
    {
        public string Email { get; set; }
        public int  GroupId { get; set; }
        public Group  Group { get; set; }

        public IList<SingleValueEntry> SingleValueEntries { get; set; }

        public Contact()
        {
            this.SingleValueEntries = new List<SingleValueEntry>();
        }
    }
}
