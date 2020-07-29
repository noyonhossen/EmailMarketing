using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWork
{
    public interface IGroupUnitOfWork : IUnitOfWork
    {
        IGroupRepository GroupRepository { get; set; }
    }
}
