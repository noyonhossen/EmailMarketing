using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWork.Smtp
{
    public interface ISMTPUnitOfWork : IUnitOfWork
    {
        public ISMTPRepository SMTPRepository { get; set; }
    }
}
