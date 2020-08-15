using EmailMarketing.Data;
using EmailMarketing.Framework.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Entities.Groups
{
    public class Group : IAuditableEntity<int>
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }

        //public IList<Contact> Contacts { get; set; }
        public IList<ContactGroup> ContactGroups { get; set; }
        public IList<ContactUploadGroup> ContactUploadGroups { get; set; }

        public Group()
        {
            //this.Contacts = new List<Contact>();
            this.ContactGroups = new List<ContactGroup>();
            this.ContactUploadGroups = new List<ContactUploadGroup>();
        }
    }
}
