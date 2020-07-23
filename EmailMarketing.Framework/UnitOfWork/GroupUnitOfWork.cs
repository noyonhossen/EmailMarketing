using EmailMarketing.Framework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWork
{
    public class GroupUnitOfWork : EmailMarketing.Data.UnitOfWork, IGroupUnitOfWork
    {
        public GroupUnitOfWork(FrameworkContext dbContext) : base(dbContext)
        {

        }
    }
}
