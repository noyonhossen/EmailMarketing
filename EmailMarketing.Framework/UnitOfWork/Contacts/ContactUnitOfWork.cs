using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWork.Contacts
{
    public class ContactUnitOfWork : EmailMarketing.Data.UnitOfWork, IContactUnitOfWork
    {
        public IContactRepository ContactRepository { get; set; }
        public ContactUnitOfWork(FrameworkContext dbContext, IContactRepository contactRepository) : base(dbContext)
        {
            ContactRepository = contactRepository;
        }
    }
}
