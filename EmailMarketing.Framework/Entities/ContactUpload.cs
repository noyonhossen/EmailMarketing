using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Entities
{
    public class ContactUpload : IAuditableEntity<int>
    {
        public string FileUrl { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public bool IsSucceed { get; set; }
        public bool IsExistingUpdate { get; set; }
        public bool HasColumnHeader { get; set; }
        public bool IsSendEmail { get; set; }
        public int SucceedEntryCount { get; set; }

        public IList<ContactUploadFieldMap> ContactUploadFieldMaps { get; set; }

        public ContactUpload()
        {
            this.ContactUploadFieldMaps = new List<ContactUploadFieldMap>();
        }
    }
}
