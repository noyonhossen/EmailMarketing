using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Membership.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models.AdminModels
{
    public class AdminUsersShowProfileModel : AdminBaseModel
    {
      
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        private readonly ApplicationUserService _applicationUserService;
        private readonly ICurrentUserService _currentUserService;

        public AdminUsersShowProfileModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<ApplicationUserService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
        }
        public AdminUsersShowProfileModel(ApplicationUserService applicationUserService, ICurrentUserService currentUserService)
        {
            _applicationUserService = applicationUserService;
            _currentUserService = currentUserService;

        }
        internal async Task ShowProfileAsync()
        {

            var currentUserId = _currentUserService.UserId;
            var adminuser =await _applicationUserService.GetByIdAsync(currentUserId);
            this.FullName = adminuser.FullName;
            this.UserName = adminuser.UserName;
            this.DateOfBirth = DateTime.UtcNow;
            //this.DateOfBirth = (DateTime)adminuser.DateOfBirth;
            this.PhoneNumber = adminuser.PhoneNumber;
            this.Gender = adminuser.Gender;
            this.Address = adminuser.Address;


        }

        
    }
}

