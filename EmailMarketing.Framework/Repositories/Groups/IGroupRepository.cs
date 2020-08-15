using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Groups
{
    public interface IGroupRepository : IRepository<Group, int, FrameworkContext>
    {
    }
}
