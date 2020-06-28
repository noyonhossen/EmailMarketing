using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EmailMarketing.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        protected readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext) => _dbContext = dbContext;

        public void SaveChanges() => _dbContext?.SaveChanges();
        public Task SaveChangesAsync() => _dbContext?.SaveChangesAsync();

        #region Dispose
        //public void Dispose() => _dbContext?.Dispose();

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _dbContext?.Dispose();
            }
            _disposed = true;
        }
        #endregion
    }
}
