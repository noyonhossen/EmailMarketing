using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.SMTP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.SMTP
{
    public interface ISMTPService:IDisposable
    {
        Task<(IList<SMTPConfig> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText,
            string orderBy,
            int pageIndex,
            int pageSize);

        Task<SMTPConfig> GetByIdAsync(Guid id);
        Task AddAsync(SMTPConfig entity);
        Task UpdateAsync(SMTPConfig entity);
        Task<SMTPConfig> DeleteAsync(Guid id);
        Task<IList<SMTPConfig>> GetAllSMTPConfig(Guid? userId);
    }
}
