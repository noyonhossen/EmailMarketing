using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWorks.Contacts
{
    public class ContactExportUnitOfWork : UnitOfWork,IContactExportUnitOfWork
    {
        public IContactExportRepository ContactExportRepository { get; set; }

        public ContactExportUnitOfWork(FrameworkContext dbContext,
            IContactExportRepository contactExportRepository) : base(dbContext)
        {
            this.ContactExportRepository = contactExportRepository;
        }
    }
}
