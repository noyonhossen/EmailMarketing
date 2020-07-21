using EmailMarketing.Data.Exceptions;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;


using System.Linq;
using EmailMarketing.Data.Extensions;

namespace EmailMarketing.Framework.Services
{
    public class GroupService: IGroupService, IDisposable
    {
        private IGroupUnitOfWork _groupUnitOfWork;

        public GroupService(IGroupUnitOfWork groupUnitOfWork)
        {
            _groupUnitOfWork = groupUnitOfWork;
        }

        //public void CreateGroup(Group group)
        //{
        //    var count = _groupUnitOfWork.GroupRepositroy.GetCount(x => x.Name == group.Name);
        //    //if (count > 0)
        //    //    throw new DuplicationException("Group Name already exists", nameof(group.Name));

        //    _groupUnitOfWork.GroupRepositroy.Add(group);
        //    _groupUnitOfWork.Save();
        //}

        //public Group DeleteGroup(int id)
        //{
        //    var group = _groupUnitOfWork.GroupRepository.GetById(id);
        //    _groupUnitOfWork.GroupRepository.Delete(group);
        //    _groupUnitOfWork.Save();
        //    return group;
        //}





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

        //public void EditGroup(Group group)
        //{
        //    var count = _groupUnitOfWork.GroupRepositroy.GetCount(x => x.Name == group.Name
        //            && x.Id != group.Id);

        //    //if (count > 0)
        //    //    throw new DuplicationException("Group Name already exists", nameof(group.Name));

        //    var existingGroup = _groupUnitOfWork.GroupRepositroy.GetById(group.Id);
        //    existingGroup.Name = group.Name;

        //    _groupUnitOfWork.Save();
        //}

        //public Group GetGroup(int id)
        //{
        //    return _groupUnitOfWork.GroupRepositroy.GetById(id);
        //}

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
