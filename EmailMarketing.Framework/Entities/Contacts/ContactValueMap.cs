using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Entities.Contacts
{
    public class ContactValueMap : IEntity<int>
    {
        //public string Name { get; set; }
        public string Value { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public int FieldMapId { get; set; }
        public FieldMap FieldMap { get; set; }
    }
}
