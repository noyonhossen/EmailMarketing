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
        public string Email { get; set; }

        private readonly ApplicationUserManager _userManager;
        private readonly AppSettings _userDefaultPassword;

        public EditUserModel()
        {
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
        }
        public EditUserModel(ApplicationUserManager userManager, IOptions<AppSettings> userDefaultPassword)
        {
            _userManager = userManager;
            _userDefaultPassword = userDefaultPassword.Value;
        }

        public async Task LoadByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Email = user.Email;

        }

        public async Task UpdateAsync()
        {
            var user = await _userManager.FindByIdAsync(this.Id.ToString());
            user.UserName = this.UserName;
            user.Email = this.Email;
            await _userManager.UpdateAsync(user);
        }
        public async Task<string> UpdatePasswordHash(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var newPassword = _userManager.PasswordHasher.HashPassword(user, _userDefaultPassword.DefaultPassword);
            user.PasswordHash = newPassword;
            await _userManager.UpdateAsync(user);
            return user.UserName;
        }
        public async Task<string> BlockUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.IsBlocked = true;
            await _userManager.UpdateAsync(user);
            return user.UserName;
        }
    }
}
