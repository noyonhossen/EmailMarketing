﻿using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Repositories.Group
{
    public interface IGroupRepository : IRepository<Entities.Group, int, FrameworkContext>
    {
    }
}
