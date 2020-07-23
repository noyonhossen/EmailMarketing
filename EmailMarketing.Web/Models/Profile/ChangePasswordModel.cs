using Autofac;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Models.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Models
{
    public class ChangePasswordModel : ProfileBaseModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "Do not match with new password")]
        public string ConfirmNewPassword { get; set; }


        public ChangePasswordModel() : base() { }
        public ChangePasswordModel(ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor)
        : base(userManager, httpContextAccessor) { }


        public async Task ChangePasswordAsync(ApplicationUser user)
        {
            var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);

            if (result.Succeeded)
            {
                user.LastPassword = CurrentPassword;
                user.PasswordChangedCount++;
            }
        }
    }
}
