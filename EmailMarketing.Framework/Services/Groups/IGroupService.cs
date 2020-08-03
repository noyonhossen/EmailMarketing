﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmailMarketing.Framework.Entities;

namespace EmailMarketing.Framework.Services.Groups
{
    public interface IGroupService:IDisposable
    {
        Task<(IList<Entities.Group> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText,
            string orderBy,
            int pageIndex,
            int pageSize);

        Task<Entities.Group> GetByIdAsync(int id);
        Task AddAsync(Entities.Group entity);
        Task UpdateAsync(Entities.Group entity);
        Task<Group> DeleteAsync(int id);
    }
}