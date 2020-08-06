using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Smtp
{
    public class SMTPRepository : Repository<Entities.SMTPConfig, Guid, FrameworkContext>, ISMTPRepository
    {
        public SMTPRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }    
    }
}
