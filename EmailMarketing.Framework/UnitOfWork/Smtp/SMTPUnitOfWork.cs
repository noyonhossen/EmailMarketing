using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories.Smtp;
using EmailMarketing.Framework.UnitOfWork.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWork
{
    public class SMTPUnitOfWork : EmailMarketing.Data.UnitOfWork, ISMTPUnitOfWork
    {
        public ISMTPRepository SMTPRepository { get; set; }
        public SMTPUnitOfWork(FrameworkContext dbContext, ISMTPRepository smtpRepository) : base(dbContext)
        {
            SMTPRepository = smtpRepository;
        }
    }
}
