using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWork
{
    public class GroupUnitOfWork: EmailMarketing.Data.UnitOfWork, IGroupUnitOfWork
    {
        public IGroupRepository GroupRepository { get; set; }
        public GroupUnitOfWork(FrameworkContext context, IGroupRepository groupRepositroy)
            : base(context)
        {
            GroupRepository = groupRepositroy;
        }
    }
}
