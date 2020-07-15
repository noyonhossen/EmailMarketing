using Autofac;
using EmailMarketing.Data;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Enums;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
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

        public EditUserModel()
        {
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
        }
        public EditUserModel(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        //public IList<ExpenseTypeSelect> ExpenseTypeSelectList => Enum.GetValues(typeof(ExpenseType))
        //                                                            .Cast<ExpenseType>()
        //                                                            .Select(p => new ExpenseTypeSelect { Value = (int)p, Text = p.ToString() })
        //                                                            .ToList();

        public async Task LoadByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            //var result = await _expenseService.GetByIdAsync(id);
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Email = user.Email;
            //this.EmailConfirmed = user.EmailConfirmed.ToString();
            //this.PhoneNumber = result.PhoneNumber;
        }

        public async Task UpdateAsync()
        {
            //var user = new ApplicationUser
            //{
            //    Id = this.Id,
            //    UserName = this.UserName,
            //    Email = this.Email,
            //    //EmailConfirmed = bool.Parse(this.EmailConfirmed)

            //};
            var user = await _userManager.FindByIdAsync(this.Id.ToString());
            user.UserName = this.UserName;
            user.Email = this.Email;
            await _userManager.UpdateAsync(user);
            //var entity = new Expense
            //{
            //    Id = this.Id,
            //    UserName = this.UserName,
            //    Email = this.Email,
            //    EmailConfirmed = this.EmailConfirmed,
            //    PhoneNumber = this.PhoneNumber
            //};
            //await _expenseService.UpdateAsync(entity);
        }
    }
}
