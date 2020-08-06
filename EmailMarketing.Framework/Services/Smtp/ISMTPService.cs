using EmailMarketing.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Smtp
{
    public interface ISmtpService:IDisposable
    {
        Task<(IList<Entities.SMTPConfig> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText,
            string orderBy,
            int pageIndex,
            int pageSize);

        Task<Entities.SMTPConfig> GetByIdAsync(Guid id);
        Task AddAsync(Entities.SMTPConfig entity);
        Task UpdateAsync(Entities.SMTPConfig entity);
        Task<SMTPConfig> DeleteAsync(Guid id);
    }
}
