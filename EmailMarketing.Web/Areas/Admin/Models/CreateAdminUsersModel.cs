using Autofac;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class CreateAdminUsersModel : AdminBaseModel
    {
        [Required]       
        public string FullName { get; set; }       
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber {get;set;}
     

        private readonly ApplicationUserManager _userManager;
        public CreateAdminUsersModel()
        {
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
        }
        public CreateAdminUsersModel(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        internal async Task CreateAdmin()
        {
            var user = new ApplicationUser
            {
                FullName = this.FullName,
                UserName = this.UserName,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user,this.Password);
        }
    }
}
