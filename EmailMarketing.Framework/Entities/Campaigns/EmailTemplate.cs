using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Entities.Campaigns
{
    public class EmailTemplate : IAuditableEntity<int>
    {
        public Guid UserId { get; set; }
        public string EmailTemplateName { get; set; }
        public string EmailTemplateBody { get; set; }
    }
}
