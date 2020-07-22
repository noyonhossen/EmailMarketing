using EmailMarketing.Data.Exceptions;
using EmailMarketing.GroupModule.Entities;
using EmailMarketing.GroupModule.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;


using System.Linq;
using EmailMarketing.Data.Extensions;

namespace EmailMarketing.GroupModule.Services
{
    public class GroupService: IGroupService, IDisposable
    {
        private IGroupUnitOfWork _groupUnitOfWork;

        public GroupService(IGroupUnitOfWork groupUnitOfWork)
        {
            _groupUnitOfWork = groupUnitOfWork;
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

        public async Task<(IList<Group> records, int total, int totalDisplay)> GetAllAsync(int pageIndex, int pageSize, string searchText, string sortText)
        {
            //var result = _groupUnitOfWork.GroupRepositroy.GetAsync().ToList();
            //return (result, 0, 0);

            var columnsMap = new Dictionary<string, Expression<Func<Entities.Group, object>>>()
            {
                ["name"] = v => v.Name
            };

            var result = await _groupUnitOfWork.GroupRepository.GetAsync<Entities.Group>(
                x => x, x => x.Name.Contains(searchText),
                x => x.ApplyOrdering(columnsMap, sortText), null,
                pageIndex, pageSize, true);

            return (result.Items, result.Total, result.TotalFilter);
        }
    }
}
