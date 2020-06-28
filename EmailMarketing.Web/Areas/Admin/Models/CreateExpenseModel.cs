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
    public class CreateExpenseModel : ExpenseBaseModel
    {
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

        public CreateExpenseModel(IExpenseService expenseService) : base(expenseService) { }
        public CreateExpenseModel() : base() { }

        public async Task AddAsync()
        {
            var entity = new Expense
            {
                Title = this.Title,
                Description = this.Description,
                Amount = this.Amount.Value,
                ExpenseType = this.ExpenseType,
                ExpenseDate = this.ExpenseDate.Value
            };
            await _expenseService.AddAsync(entity);
        }
    }
}
