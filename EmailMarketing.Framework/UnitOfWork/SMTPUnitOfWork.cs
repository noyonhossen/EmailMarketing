using EmailMarketing.Framework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWork
{
    public class SMTPUnitOfWork : EmailMarketing.Data.UnitOfWork, ISMTPUnitOfWork
    {
        public SMTPUnitOfWork(FrameworkContext dbContext) : base(dbContext)
        {

        }
    }
}
