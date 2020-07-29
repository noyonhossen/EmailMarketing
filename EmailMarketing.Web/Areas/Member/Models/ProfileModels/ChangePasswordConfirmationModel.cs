using EmailMarketing.Common.Services;
using EmailMarketing.Membership.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.ProfileModels
{
    public class ChangePasswordConfirmationModel : MemberBaseModel
    {
        public ChangePasswordConfirmationModel() : base() { }
        public ChangePasswordConfirmationModel(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }
        
        
    }
}
