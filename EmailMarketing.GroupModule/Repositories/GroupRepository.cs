using EmailMarketing.Data;
using EmailMarketing.GroupModule.Context;
using EmailMarketing.GroupModule.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.GroupModule.Repositories
{
    public class GroupRepository: Repository<Group, int, GroupContext>, IGroupRepository
    {
        public GroupRepository(GroupContext dbContext)
            : base(dbContext)
        {

        }
    }
}
