using EmailMarketing.Data;
using EmailMarketing.GroupModule.Context;
using EmailMarketing.GroupModule.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.GroupModule.UnitOfWork
{
    public class GroupUnitOfWork: EmailMarketing.Data.UnitOfWork, IGroupUnitOfWork
    {
        public IGroupRepository GroupRepository { get; set; }
        public GroupUnitOfWork(GroupContext context, IGroupRepository groupRepositroy)
            : base(context)
        {
            GroupRepository = groupRepositroy;
        }
    }
}
