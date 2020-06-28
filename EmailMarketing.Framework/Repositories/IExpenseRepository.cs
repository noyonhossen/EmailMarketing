using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Repositories
{
    public interface IExpenseRepository : IRepository<Entities.Expense, int, FrameworkContext>
    {
        
    }
}
