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
        public string EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }

        private readonly ApplicationUserManager _userManager;

        public EditUserModel()
        {

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
            var result = await _userManager.FindByIdAsync(id.ToString());
            //var result = await _expenseService.GetByIdAsync(id);
            this.Id = result.Id;
            this.UserName = result.UserName;
            this.Email = result.Email;
            this.EmailConfirmed = result.EmailConfirmed.ToString();
            //this.PhoneNumber = result.PhoneNumber;
        }

        public async Task UpdateAsync()
        {
            var entity = new ApplicationUser
            {
                Id = this.Id,
                UserName = this.UserName,
                Email = this.Email,
                EmailConfirmed = bool.Parse(this.EmailConfirmed)

            };
            await _userManager.UpdateAsync(entity);
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
