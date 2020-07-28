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

namespace EmailMarketing.Web.Areas.Admin.Models.AdminModels
{
    public class CreateAdminUsersModel : AdminBaseModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }    
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }


        private readonly ApplicationUserService _applicationUserService;
        private readonly AppSettings _userDefaultPassword;
        public CreateAdminUsersModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<ApplicationUserService>();
            _userDefaultPassword = Startup.AutofacContainer.Resolve<IOptions<AppSettings>>().Value;
        }
        public CreateAdminUsersModel( ApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        internal async Task CreateAdminAsync()
        {
            var user = new ApplicationUser
            {
                FullName = this.FullName,
                UserName = this.UserName,
                Email = this.UserName,
                PhoneNumber = this.PhoneNumber,
                Gender = this.Gender,
                DateOfBirth = this.DateOfBirth,
                Address = this.Address,
                EmailConfirmed = true


            };
            var userRoleName = ConstantsValue.UserRoleName.Admin;
            var password = _userDefaultPassword.UserDefaultPassword;
            await _applicationUserService.AddAsync(user, userRoleName,password);
        }
    }
}
