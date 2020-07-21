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

            //var adminUser = await _userManager.GetUserAsync(User);
            //adminUser.FullName = this.FullName;
            //adminUser.UserName= this.UserName;
            //adminUser.Email= this.Email;
            //adminUser.PhoneNumber = this.PhoneNumber;

            //var result = await _userManager.UpdateAsync();
        }
    }
}

