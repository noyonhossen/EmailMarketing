using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Entities.Contacts
{
    public class ContactUploadFieldMap : IEntity<int>
    {
        public int Index { get; set; }
        public int FieldMapId { get; set; }
        public FieldMap FieldMap { get; set; }
        public int ContactUploadId { get; set; }
        public ContactUpload ContactUpload { get; set; }
    }
}
