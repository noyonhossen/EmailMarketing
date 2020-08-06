using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models
{
    public class ContactModel : MemberBaseModel
    {
        public IFormFile FileUrl { get; set; }
        public int GroupId { get; set; }
        public ContactModel() : base(){ }
    }
}
