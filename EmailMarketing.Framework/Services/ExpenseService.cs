using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmailMarketing.Common.Extensions;
using EmailMarketing.Common.Exceptions;

namespace EmailMarketing.Framework.Services
{
    public class ExpenseService : IExpenseService
    {
        private IExpenseUnitOfWork _courseUnitOfWork;

        public ExpenseService(IExpenseUnitOfWork courseUnitOfWork)
        {
            _courseUnitOfWork = courseUnitOfWork;
        }

        public async Task<(IList<Entities.Expense> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Entities.Expense, object>>>()
            {
                ["title"] = v => v.Title,
                ["expenseType"] = v => v.ExpenseType,
                ["expenseDate"] = v => v.ExpenseDate,
                ["amount"] = v => v.Amount
            };

            var result = await _courseUnitOfWork.ExpenseRepository.GetAsync<Entities.Expense>(
                x => x, x => x.Title.Contains(searchText),
                x => x.ApplyOrdering(columnsMap, orderBy), null, 
                pageIndex, pageSize, true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public async Task<Entities.Expense> GetByIdAsync(int id)
        {
            return await _courseUnitOfWork.ExpenseRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Entities.Expense entity)
        {
            var isExists = await _courseUnitOfWork.ExpenseRepository.IsExistsAsync(x => x.Title == entity.Title && x.Id != entity.Id);
            if(isExists)
                throw new DuplicationException(nameof(entity.Title));

            await _courseUnitOfWork.ExpenseRepository.AddAsync(entity);
            await _courseUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Entities.Expense entity)
        {
            var isExists = await _courseUnitOfWork.ExpenseRepository.IsExistsAsync(x => x.Title == entity.Title && x.Id != entity.Id);
            if (isExists)
                throw new DuplicationException(nameof(entity.Title));

            var updateEntity = await GetByIdAsync(entity.Id);
            updateEntity.Title = entity.Title;
            updateEntity.Description = entity.Title;
            updateEntity.Amount = entity.Amount;
            updateEntity.ExpenseDate = entity.ExpenseDate;
            updateEntity.ExpenseType = entity.ExpenseType;

            await _courseUnitOfWork.ExpenseRepository.UpdateAsync(updateEntity);
            await _courseUnitOfWork.SaveChangesAsync();
        }

        public async Task<Expense> DeleteAsync(int id)
        {
            var expense = await GetByIdAsync(id);
            await _courseUnitOfWork.ExpenseRepository.DeleteAsync(id);
            await _courseUnitOfWork.SaveChangesAsync();
            return expense;
        }

        public void Dispose()
        {
            _courseUnitOfWork?.Dispose();
        }
    }
}
