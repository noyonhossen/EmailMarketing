using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWorks.Groups
{
    public interface IGroupUnitOfWork : IUnitOfWork
    {
        IGroupRepository GroupRepository { get; set; }
    }
}
