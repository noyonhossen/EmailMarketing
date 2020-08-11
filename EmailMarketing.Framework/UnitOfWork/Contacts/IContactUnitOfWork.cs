using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWork.Contacts
{
    public interface IContactUnitOfWork : IUnitOfWork
    {
        IContactRepository ContactRepository { get; set; }
    }
}
