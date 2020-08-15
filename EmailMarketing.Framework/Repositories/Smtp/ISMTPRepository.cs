using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities.SMTP;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.SMTP
{
    public interface ISMTPRepository : IRepository<SMTPConfig, Guid, FrameworkContext>
    {
    }
}
