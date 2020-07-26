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
    public class MemberEditUserModel : AdminBaseModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string LastPassword { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBlocked { get; set; }

        private readonly ApplicationUserService _applicationUserService;

        public MemberEditUserModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<ApplicationUserService>();
        }
        public MemberEditUserModel(ApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
            
        }

        public async Task LoadByIdAsync(Guid id)
        {
            var user = await _applicationUserService.GetByIdAsync(id);
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
            var user = await _applicationUserService.GetByIdAsync(this.Id);
            user.UserName = this.UserName;
            user.Email = this.Email;
            user.PhoneNumber = this.PhoneNumber;
            user.FullName = this.FullName;
            user.Address = this.Address;
            user.Gender = this.Gender;
            await _applicationUserService.UpdateAsync(user);
        }
        
    }
}
