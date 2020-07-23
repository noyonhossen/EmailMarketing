using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Entities
{
    public class Group : IAuditableEntity<int>
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public IList<Contact> Contacts { get; set; }

        public Group()
        {
            this.Contacts = new List<Contact>();
        }
    }
}
