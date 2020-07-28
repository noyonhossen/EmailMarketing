using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models.AdminModels
{
    public class AdminUsersProfileUpdateModel : AdminBaseModel
    {
        
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        //public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        

        private readonly ApplicationUserService _applicationUserService;
        private readonly ICurrentUserService _currentUserService;

        public AdminUsersProfileUpdateModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<ApplicationUserService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
        }
        public AdminUsersProfileUpdateModel(ApplicationUserService applicationUserService, ICurrentUserService currentUserService)
        {
            _applicationUserService = applicationUserService;
            _currentUserService = currentUserService;

        }
        internal async Task UpdateProfileAsync()
        {

            var currentUserId = _currentUserService.UserId;
            var adminuser = await _applicationUserService.GetByIdAsync(currentUserId);
            adminuser.FullName = this.FullName;
            adminuser.UserName = this.UserName;
            adminuser.PhoneNumber = this.PhoneNumber;
            adminuser.Gender = this.Gender;
            adminuser.Address = this.Address;

            var result = await _applicationUserService.UpdateAsync(adminuser);
        }
    }
}

