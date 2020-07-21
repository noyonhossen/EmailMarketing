using Autofac;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class AdminUsersProfileUpdateModel : AdminBaseModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        private readonly ApplicationUserManager _userManager;
        public AdminUsersProfileUpdateModel()
        {
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
        }
        public AdminUsersProfileUpdateModel(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        internal async Task UpdateProfileAsync()
        {

            var adminuser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            adminuser.FullName = this.FullName;
            adminuser.UserName = this.UserName;
            adminuser.Email = this.Email;
            adminuser.PhoneNumber = this.PhoneNumber;

            var result = await _userManager.UpdateAsync(adminuser);
        }
    }
}

