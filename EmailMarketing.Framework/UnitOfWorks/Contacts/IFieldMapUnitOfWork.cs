using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWorks.Contacts
{
    public interface IFieldMapUnitOfWork : IUnitOfWork
    {
        IFieldMapRepository FieldMapRepository { get; set; }
    }
}
