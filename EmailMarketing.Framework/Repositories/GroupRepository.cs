using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories
{
    public class GroupRepository: Repository<Group, int, FrameworkContext>, IGroupRepository
    {
        public GroupRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
