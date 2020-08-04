using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Smtp
{
    public interface ISMTPRepository : IRepository<Entities.SMTPConfig, Guid, FrameworkContext>
    {
    }
}
