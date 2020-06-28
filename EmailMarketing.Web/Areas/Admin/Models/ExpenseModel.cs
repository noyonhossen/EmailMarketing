using EmailMarketing.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class ExpenseModel : ExpenseBaseModel
    {
        public ExpenseModel(IExpenseService expenseService) : base(expenseService) { }
        public ExpenseModel() : base() { }

        public async Task<object> GetAllAsync(DataTablesAjaxRequestModel tableModel)
        {
            var result = await _expenseService.GetAllAsync(
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "title", "expenseType", "expenseDate", "amount" }),
                tableModel.PageIndex, tableModel.PageSize);

            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,
                data = (from item in result.Items
                        select new string[]
                        {
                                    item.Title,
                                    item.ExpenseType.ToString(),
                                    item.ExpenseDate.ToString("dd MMMM, yyyy"),
                                    item.Amount.ToString(),
                                    item.Id.ToString()
                        }
                        ).ToArray()

            };
        }

        public async Task<string> DeleteAsync(int id)
        {
            var expense = await _expenseService.DeleteAsync(id);
            return expense.Title;
        }
    }
}
