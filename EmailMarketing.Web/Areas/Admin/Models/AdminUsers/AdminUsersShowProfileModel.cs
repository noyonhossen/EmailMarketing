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

namespace EmailMarketing.Web.Areas.Admin.Models.AdminUsers
{
    public class AdminUsersShowProfileModel : AdminBaseModel
    {
      
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        private readonly IApplicationUserService _applicationUserService;
        private readonly ICurrentUserService _currentUserService;

        public AdminUsersShowProfileModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
        }

        public AdminUsersShowProfileModel(IApplicationUserService applicationUserService, ICurrentUserService currentUserService)
        {
            _applicationUserService = applicationUserService;
            _currentUserService = currentUserService;
        }

        public async Task ShowProfileAsync()
        {
            var adminuser =await _applicationUserService.GetByIdAsync(_currentUserService.UserId);
            this.FullName = adminuser.FullName;
            this.Email = adminuser.Email;
            this.DateOfBirth = adminuser.DateOfBirth;
            this.PhoneNumber = adminuser.PhoneNumber;
            this.Gender = adminuser.Gender;
            this.Address = adminuser.Address;
        }
    }
}

