using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories.SMTP;
using EmailMarketing.Framework.UnitOfWorks.SMTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWorks.SMTP
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
