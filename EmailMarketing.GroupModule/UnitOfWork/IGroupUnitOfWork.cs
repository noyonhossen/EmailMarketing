using EmailMarketing.Data;
using EmailMarketing.GroupModule.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.GroupModule.UnitOfWork
{
    public interface IGroupUnitOfWork:IUnitOfWork
    {
        IGroupRepository GroupRepository { get; set; }
    }

   
}
