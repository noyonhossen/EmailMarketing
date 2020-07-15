using Autofac;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class UserInformationModel : AdminBaseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        private readonly ApplicationUserManager _userManager;

        public UserInformationModel()
        {
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
        }
        public UserInformationModel(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task LoadByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            //var result = await _expenseService.GetByIdAsync(id);
            this.UserName = user.UserName;
            this.Email = user.Email;
            //this.EmailConfirmed = user.EmailConfirmed.ToString();
            //this.PhoneNumber = result.PhoneNumber;
        }
    }
}
