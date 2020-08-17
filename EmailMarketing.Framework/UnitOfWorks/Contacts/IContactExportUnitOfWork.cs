using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWorks.Contacts
{
    public interface IContactExportUnitOfWork : IUnitOfWork
    {
        IContactExportRepository ContactExportRepository { get; set; }
    }
}
