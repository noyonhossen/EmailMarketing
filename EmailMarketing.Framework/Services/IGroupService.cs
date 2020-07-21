using EmailMarketing.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services
{
    public interface IGroupService:IDisposable
    {
        Task<(IList<Group> records, int total, int totalDisplay)> GetAllAsync(int pageIndex,
                                                                    int pageSize,
                                                                    string searchText,
                                                                    string orderBy);
        //void CreateGroup(Group group);
        //void EditGroup(Group group);
        //Group GetGroup(int id);
        //Group DeleteGroup(int id);

        

        Task<Entities.Group> GetByIdAsync(int id);
        Task AddAsync(Entities.Group entity);
        Task UpdateAsync(Entities.Group entity);
        Task<Group> DeleteAsync(int id);

    }
}
