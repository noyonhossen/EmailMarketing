using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWork.Contacts
{
    public class ContactExcelUnitOfWork : EmailMarketing.Data.UnitOfWork, IContactExcelUnitOfWork
    {
        public IContactRepository ContactRepository { get; set; }
        public IContactUploadRepository ContactUploadRepository { get; set; }
        public IContactValueMapRepository ContactValueMapRepository { get; set; }

        public ContactExcelUnitOfWork(FrameworkContext dbContext,
            IContactRepository contactRepository, IContactUploadRepository contactUploadRepository, IContactValueMapRepository contactValueMapRepository) : base(dbContext)
        {
            this.ContactRepository = contactRepository;
            this.ContactUploadRepository = contactUploadRepository;
            this.ContactValueMapRepository = contactValueMapRepository;
        }
    }
}
