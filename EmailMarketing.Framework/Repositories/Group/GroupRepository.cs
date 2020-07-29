using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Group
{
    public class GroupRepository : Repository<Entities.Group, int, FrameworkContext>, IGroupRepository
    {
        public GroupRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
