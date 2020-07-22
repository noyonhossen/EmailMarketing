using EmailMarketing.GroupModule.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.GroupModule.Services
{
    public interface IGroupService:IDisposable
    {
        Task<(IList<Group> records, int total, int totalDisplay)> GetAllAsync(int pageIndex,
                                                                    int pageSize,
                                                                    string searchText,
                                                                    string orderBy);
        
        Task<Entities.Group> GetByIdAsync(int id);
        Task AddAsync(Entities.Group entity);
        Task UpdateAsync(Entities.Group entity);
        Task<Group> DeleteAsync(int id);

    }
}
