using EmailMarketing.Data;
using EmailMarketing.Framework.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Entities
{
    public class ContactGroup : IEntity<int>
    {
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
