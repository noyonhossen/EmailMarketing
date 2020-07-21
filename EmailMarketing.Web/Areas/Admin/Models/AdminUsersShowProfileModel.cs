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

            //var adminUser = await _userManager.GetUserAsync(User);
            //this.FullName = adminUser.FullName;
            //this.UserName = adminUser.UserName;
            //this.Email = adminUser.Email;
            //this.PhoneNumber = adminUser.PhoneNumber;

            
        }

        
    }
}

