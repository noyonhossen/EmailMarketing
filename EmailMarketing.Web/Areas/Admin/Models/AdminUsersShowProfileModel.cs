using Autofac;
using EmailMarketing.Membership.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class AdminUsersShowProfileModel : AdminBaseModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ApplicationUserManager _userManager;
       // private readonly UserManager<ApplicationUser> _userManager;
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
            
            var adminuser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            this.FullName = adminuser.FullName;
            this.UserName = adminuser.UserName;
            this.Email = adminuser.Email;
            this.PhoneNumber = adminuser.PhoneNumber;


        }

        
    }
}

