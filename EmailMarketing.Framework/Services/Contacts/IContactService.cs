
﻿using System;
using System.Collections.Generic;
using System.Text;
﻿using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Contacts;
using System.Linq;
using System.Text.RegularExpressions;
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

        Task<int> GroupContactCountAsync(int id);
        Task<IList<(int Value, string Text,int Count)>> GetAllGroupsAsync(Guid? userId);
        Task<IList<(int Value, string Text)>> GetAllContactValueMaps(Guid? userId);
        Task AddContact(Contact contact);
        Task<IList<(int Value, string Text)>> GetAllContactValueMapsCustom(Guid? userId);
        Task AddContacValueMaps(IList<ContactValueMap> contactValueMap);
        Task AddContactGroups(IList<ContactGroup> contactGroups);
        Task<int> GetIdByEmail(string email);
        Task UpdateAsync(Contact contact);
    }
}
