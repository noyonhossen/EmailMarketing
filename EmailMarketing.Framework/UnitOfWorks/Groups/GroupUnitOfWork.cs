using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWorks.Groups
{
    public class GroupUnitOfWork : EmailMarketing.Data.UnitOfWork, IGroupUnitOfWork
    {
        public IGroupRepository GroupRepository { get; set; }
        public GroupUnitOfWork(FrameworkContext dbContext, IGroupRepository groupRepository) : base(dbContext)
        {
            GroupRepository = groupRepository;
        }

        
    }
}
