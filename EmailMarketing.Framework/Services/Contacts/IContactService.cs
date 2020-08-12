﻿using EmailMarketing.Framework.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Contacts
{
    public interface IContactService : IDisposable
    {
        Task<int> GroupContactCountAsync(int id);
        Task<IList<(int Value, string Text)>> GetAllGroupsAsync(Guid? userId);
        Task<IList<(int Value, string Text)>> GetAllContactValueMaps(Guid? userId);
        Task AddContact(Contact contact);
        Task<IList<(int Value, string Text)>> GetAllContactValueMapsCustom(Guid? userId);
    }
}
