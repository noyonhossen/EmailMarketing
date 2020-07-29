using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models.AdminUsers
{
    public class AdminUsersProfileUpdateModel : AdminBaseModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM, yyyy}")]
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        

        private readonly IApplicationUserService _applicationUserService;
        private readonly ICurrentUserService _currentUserService;

        public AdminUsersProfileUpdateModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
        }

        public AdminUsersProfileUpdateModel(IApplicationUserService applicationUserService, ICurrentUserService currentUserService)
        {
            _applicationUserService = applicationUserService;
            _currentUserService = currentUserService;
        }

        public async Task UpdateProfileAsync()
        {
            var adminuser = new ApplicationUser
            {
                FullName = this.FullName,
                UserName = this.Email,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                Gender = this.Gender,
                DateOfBirth = this.DateOfBirth,
                Address = this.Address
            };

            var result = await _applicationUserService.UpdateAsync(adminuser);
        }
    }
}

