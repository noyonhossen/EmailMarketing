using System;
using System.Threading.Tasks;

namespace EmailMarketing.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
