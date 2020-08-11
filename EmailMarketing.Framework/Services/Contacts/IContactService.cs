using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Contacts
{
    public interface IContactService : IDisposable
    {
        Task<(IList<Entities.Contacts.Contact> Items, int Total, int TotalFilter)> GetAllContactAsync(
            Guid? userId,
            string searchText,
            string orderBy,
            int pageIndex,
            int pageSize);
        Task<Entities.Contacts.Contact> GetByIdAsync(int id);
        Task<Entities.Contacts.Contact> DeleteAsync(int id);
    }
}
