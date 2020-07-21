using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWork
{
    public interface IGroupUnitOfWork:IUnitOfWork
    {
        IGroupRepository GroupRepository { get; set; }
    }

   
}
