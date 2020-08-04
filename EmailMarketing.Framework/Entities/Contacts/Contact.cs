using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Entities.Contacts
{
    public class Contact : IAuditableEntity<int>
    {
        public string Email { get; set; }
        public int  GroupId { get; set; }
        public Group  Group { get; set; }
        public int ContactUploadId { get; set; }
        public ContactUpload ContactUpload { get; set; }

        public IList<ContactValueMap> ContactValueMaps { get; set; }

        public Contact()
        {
            this.ContactValueMaps = new List<ContactValueMap>();
        }
    }
}
