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
    public interface IExpenseUnitOfWork : IUnitOfWork
    {
        IExpenseRepository ExpenseRepository { get; set; } 
    }
}
