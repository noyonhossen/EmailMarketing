using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWork
{
    public class ExpenseUnitOfWork : EmailMarketing.Data.UnitOfWork, IExpenseUnitOfWork
    {
        public IExpenseRepository ExpenseRepository { get; set; }

        public ExpenseUnitOfWork(FrameworkContext dbContext,
            IExpenseRepository expenseRepository) : base(dbContext)
        {
            ExpenseRepository = expenseRepository;
        }
    }
}
