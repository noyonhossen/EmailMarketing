using EmailMarketing.Data;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Entities.Contacts
{
    public class ContactGroup : IEntity<int>
    {
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
