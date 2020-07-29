using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmailMarketing.Common.Extensions;
using System.Linq;
using EmailMarketing.Common.Services;

namespace EmailMarketing.Framework.Services.Groups
{
    public class GroupService:IGroupService
    { 
        
        private IGroupUnitOfWork _groupUnitOfWork;
        private ICurrentUserService _currentUserService;

        public GroupService(IGroupUnitOfWork GroupUnitOfWork, ICurrentUserService currentUserService)
        {
            _groupUnitOfWork = GroupUnitOfWork;
            _currentUserService = currentUserService;

        }
        public async Task<(IList<Entities.Group> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Entities.Group, object>>>()
            {
                ["name"] = v => v.Name
            };
            
            var result = await _groupUnitOfWork.GroupRepository.GetAsync<Entities.Group>(
                x => x, x => x.Name.Contains(searchText),
                x => x.ApplyOrdering(columnsMap, orderBy), null,
                pageIndex, pageSize, true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public async Task<Entities.Group> GetByIdAsync(int id)
        {
            return await _groupUnitOfWork.GroupRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Entities.Group entity)
        {
            var isExists = await _groupUnitOfWork.GroupRepository.IsExistsAsync(x => x.Name == entity.Name && x.Id != entity.Id);
            if (isExists)
                throw new DuplicationException(nameof(entity.Name));

            await _groupUnitOfWork.GroupRepository.AddAsync(entity);
            await _groupUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Entities.Group entity)
        {
            var isExists = await _groupUnitOfWork.GroupRepository.IsExistsAsync(x => x.Name == entity.Name && x.Id != entity.Id);
            if (isExists)
                throw new DuplicationException(nameof(entity.Name));

            var updateEntity = await GetByIdAsync(entity.Id);
            updateEntity.Name = entity.Name;

            await _groupUnitOfWork.GroupRepository.UpdateAsync(updateEntity);
            await _groupUnitOfWork.SaveChangesAsync();
        }

        public async Task<Group> DeleteAsync(int id)
        {
            var group = await GetByIdAsync(id);
            await _groupUnitOfWork.GroupRepository.DeleteAsync(id);
            await _groupUnitOfWork.SaveChangesAsync();
            return group;
        }

        public void Dispose()
    {
            _groupUnitOfWork?.Dispose();
    }
    }
}
