using Autofac;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class AdminUsersShowProfileModel : AdminBaseModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        private readonly ApplicationUserManager _userManager;
        public AdminUsersShowProfileModel()
        {
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
        }
        public AdminUsersShowProfileModel(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        internal async Task ShowProfileAsync()
        {

            //var adminuser = await _userManager.GetUserAsync(User);
            //this.FullName = adminuser.FullName;
            //this.UserName = adminuser.UserName;
            //this.Email = adminuser.Email;
            //this.PhoneNumber = adminuser.PhoneNumber;


        }

        
    }
}

