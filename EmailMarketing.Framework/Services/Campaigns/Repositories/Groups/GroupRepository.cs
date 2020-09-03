using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Groups
{
    public class GroupRepository : Repository<Group, int, FrameworkContext>, IGroupRepository
    {
        public GroupRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
