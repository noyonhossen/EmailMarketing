using Autofac;
using EmailMarketing.Data;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Enums;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Core;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class EditUserModel : AdminBaseModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string LastPassword { get; set; }
        public int PasswordChangedCount { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public string Created { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBlocked { get; set; }

        private readonly ApplicationUserManager _userManager;
 
        public EditUserModel()
        {
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
        }
        public EditUserModel(ApplicationUserManager userManager)
        {
            _userManager = userManager;
            
        }

        public async Task LoadByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.FullName = user.FullName;
            this.Address = user.Address;
            this.Gender = user.Gender;
        }

        public async Task UpdateAsync()
        {
            var user = await _userManager.FindByIdAsync(this.Id.ToString());
            user.UserName = this.UserName;
            user.Email = this.Email;
            user.PhoneNumber = this.PhoneNumber;
            user.FullName = this.FullName;
            user.Address = this.Address;
            user.Gender = this.Gender;
            await _userManager.UpdateAsync(user);
        }
        
    }
}
