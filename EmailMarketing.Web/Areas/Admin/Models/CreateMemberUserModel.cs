using Autofac;
using EmailMarketing.Membership.Constants;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Core;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class CreateMemberUserModel : AdminBaseModel
    {
        [Required]
        public string FullName { get; set; }
        
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }


        private readonly ApplicationUserService _applicationUserService;
        private readonly AppSettings _userDefaultPassword;
        public CreateMemberUserModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<ApplicationUserService>();
            _userDefaultPassword = Startup.AutofacContainer.Resolve<IOptions<AppSettings>>().Value;
        }
        public CreateMemberUserModel(ApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        internal async Task CreateUserAsync()
        {
            var user = new ApplicationUser
            {
                FullName = this.FullName,
                UserName = this.Email,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                Gender = this.Gender,
                Address = this.Address,
                EmailConfirmed = true

               
            };
            var userRoleName = ConstantsValue.UserRoleName.Member;
            var password = _userDefaultPassword.UserDefaultPassword;
            await _applicationUserService.AddAsync(user, userRoleName, password);
        }
    }
}
