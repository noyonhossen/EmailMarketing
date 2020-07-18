using Autofac;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class EditAdminUsersModel : AdminBaseModel
    {
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
     

        private readonly ApplicationUserManager _userManager;
        public EditAdminUsersModel()
        {
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
        }
        public EditAdminUsersModel(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task LoadByIdAsync(Guid Id)
        {
            var result = await _userManager.FindByIdAsync(Id.ToString());

            this.Id = result.Id;
            this.FullName = result.FullName;
            this.UserName = result.UserName;
            this.Email = result.Email;
            this.PhoneNumber = result.PhoneNumber;

        }
        public async Task UpdateAsync()
        {
           var adminuser = await _userManager.FindByIdAsync(this.Id.ToString());
            adminuser.FullName = this.FullName;
            adminuser.UserName = this.UserName;
            adminuser.Email = this.Email;
            adminuser.PhoneNumber = this.PhoneNumber;
               //await _userManager.UpdateAsync(user);
            await _userManager.UpdateAsync(adminuser);
        }
    }
}
