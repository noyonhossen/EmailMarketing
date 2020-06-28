using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Enums;
using EmailMarketing.Framework.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class EditExpenseModel : ExpenseBaseModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        [Display(Name = "Expense Type")]
        public ExpenseType ExpenseType { get; set; }
        [Required]
        [Display(Name = "Expense Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM, yyyy}")]
        public DateTime? ExpenseDate { get; set; }

        public IList<ExpenseTypeSelect> ExpenseTypeSelectList => Enum.GetValues(typeof(ExpenseType))
                                                                    .Cast<ExpenseType>()
                                                                    .Select(p => new ExpenseTypeSelect { Value = (int)p, Text = p.ToString() })
                                                                    .ToList();

        public EditExpenseModel(IExpenseService expenseService) : base(expenseService) { }
        public EditExpenseModel() : base() { }

        public async Task LoadByIdAsync(int id)
        {
            var result = await _expenseService.GetByIdAsync(id);
            this.Id = result.Id;
            this.Title = result.Title;
            this.Description = result.Description;
            this.Amount = result.Amount;
            this.ExpenseType = result.ExpenseType;
            this.ExpenseDate = result.ExpenseDate;
        }

        public async Task UpdateAsync()
        {
            var entity = new Expense
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                Amount = this.Amount.Value,
                ExpenseType = this.ExpenseType,
                ExpenseDate = this.ExpenseDate.Value
            };
            await _expenseService.UpdateAsync(entity);
        }
    }
}
