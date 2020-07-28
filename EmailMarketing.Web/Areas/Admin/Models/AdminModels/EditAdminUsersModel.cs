using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models.AdminModels
{    
    public class EditAdminUsersModel : AdminBaseModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
       
       

        private readonly ApplicationUserService _applicationUserService;
        private readonly ICurrentUserService _currentUserService;
        public EditAdminUsersModel()
        { 
            _applicationUserService = Startup.AutofacContainer.Resolve<ApplicationUserService>();
        }
        public EditAdminUsersModel(ApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task LoadByIdAsync(Guid id)
        {
            var user = await _applicationUserService.GetByIdAsync(id);
            this.Id = user.Id;
            this.FullName = user.FullName;
            this.UserName = user.UserName;
            this.DateOfBirth = (DateTime)user.DateOfBirth;
            this.PhoneNumber = user.PhoneNumber;
            this.Address = user.Address;
            this.Gender = user.Gender;
        }

        public async Task UpdateAsync()
        {
            var user = await _applicationUserService.GetByIdAsync(this.Id);
            user.FullName = this.FullName;
            user.UserName = this.UserName;
            user.DateOfBirth = this.DateOfBirth;
            user.PhoneNumber = this.PhoneNumber;
            user.Address = this.Address;
            user.Gender = this.Gender;
            await _applicationUserService.UpdateAsync(user);
        }
    }
}
