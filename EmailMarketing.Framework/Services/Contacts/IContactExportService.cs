using EmailMarketing.Framework.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Contacts
{
    public interface IContactExportService : IDisposable
    {
        Task<IList<Contact>> GetAllContactAsync(Guid? userId);
    }
}