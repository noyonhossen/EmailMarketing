
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
       Task<IList<(int Id, int Value, string Text, string Input)>> GetAllContactValueMapsCustom(Guid? userId, int contactId);
        Task<IList<(int Id, int Value, string Text, string Input)>> GetAllContactValueMaps(Guid? userId, int contactId);
        Task<(IList<Entities.Contacts.Contact> Items, int Total, int TotalFilter)> GetAllContactAsync(
            Guid? userId,
            string searchText,
            string orderBy,
            int pageIndex,
            int pageSize);
        Task<ContactValueMap> GetContactValueMapByIdAsync(int id);
        Task<Entities.Contacts.Contact> GetByIdAsync(int id);
        Task<Entities.Contacts.Contact> DeleteAsync(int id);
        Task DeleteContactGroupAsync(int id, int contactId);
        Task<int> GroupContactCountAsync(int id);
        Task<IList<(int Value, string Text,int Count)>> GetAllGroupsAsync(Guid? userId);
        Task<Entities.Groups.Group> GetGroupByIdAsync(int id);
        Task<IList<(int Value, string Text)>> GetAllContactValueMapsStandard();
        Task AddContact(Contact contact);
        Task<IList<(int Value, string Text)>> GetAllContactValueMapsCustom(Guid? userId);
        Task AddContacValueMaps(IList<ContactValueMap> contactValueMap);
        Task AddContactGroups(IList<ContactGroup> contactGroups);
        Task<Contact> GetIdByEmail(string email);
        Task UpdateAsync(Contact contact);
        Task UpdateRangeAsync(IList<ContactValueMap> contact);
    }
}
