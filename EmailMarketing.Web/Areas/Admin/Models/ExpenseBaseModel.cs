using Autofac;
using EmailMarketing.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class ExpenseBaseModel : AdminBaseModel, IDisposable
    {
        protected readonly IExpenseService _expenseService;
        public ExpenseBaseModel(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public ExpenseBaseModel()
        {
            _expenseService = Startup.AutofacContainer.Resolve<IExpenseService>();
        }

        public void Dispose()
        {
            _expenseService?.Dispose();
        }
    }
}
