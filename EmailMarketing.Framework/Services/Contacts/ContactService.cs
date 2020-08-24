
﻿using EmailMarketing.Common.Exceptions;
using EmailMarketing.Common.Extensions;

﻿using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Services.Groups;
using EmailMarketing.Framework.UnitOfWorks;
using EmailMarketing.Framework.UnitOfWorks.Contacts;
using EmailMarketing.Framework.UnitOfWorks.Groups;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Contacts
{
    public class ContactService : IContactService
    {
        private IContactUnitOfWork _contactUnitOfWork;

        private IGroupUnitOfWork _groupUnitOfWork;


        public ContactService(IContactUnitOfWork contactUnitOfWork, IGroupUnitOfWork groupUnitOfWork)
        {
            _contactUnitOfWork = contactUnitOfWork;
            _groupUnitOfWork = groupUnitOfWork;
        }

        public async Task<(IList<Contact> Items, int Total, int TotalFilter)> GetAllContactAsync(
            Guid? userId, string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Entities.Contacts.Contact, object>>>()
            {
                ["Email"] = v => v.Email
            };
            var result = await _contactUnitOfWork.ContactRepository.GetAsync<Entities.Contacts.Contact>(
                x => x, x => (!userId.HasValue || x.UserId == userId.Value) && (x.Email.Contains(searchText)),
                x => x.ApplyOrdering(columnsMap, orderBy), x => x.Include(i => i.ContactGroups).ThenInclude(i => i.Group),
                pageIndex, pageSize, true);
            return (result.Items, result.Total, result.TotalFilter);
        }
        public async Task<Contact> GetByIdAsync(int id)
        {
            var result = await _contactUnitOfWork.ContactRepository.GetFirstOrDefaultAsync(
                x => x, x => x.Id == id,
                x => x.Include(i => i.ContactGroups).ThenInclude(i => i.Group)
                        .Include(i => i.ContactValueMaps).ThenInclude(i => i.FieldMap));

            if (result == null) throw new NotFoundException(nameof(Contact), id);

            return result;
        }
        public async Task<Contact> DeleteAsync(int id)
        {
            var contact = await GetByIdAsync(id);
            if (contact == null) throw new NotFoundException(nameof(Contact), id);
            await _contactUnitOfWork.ContactRepository.DeleteAsync(id);
            await _contactUnitOfWork.SaveChangesAsync();
            return contact;
        }


        public async Task<int> GroupContactCountAsync(int id)
        {
            return await _contactUnitOfWork.GroupContactRepository.GetCountAsync();
        }

        public async Task AddContact(Contact contact)
        {
            await _contactUnitOfWork.ContactRepository.AddAsync(contact);
            await _contactUnitOfWork.SaveChangesAsync();
        }

        public async Task AddContacValueMaps(IList<ContactValueMap> contactValueMaps)
        {
            await _contactUnitOfWork.ContactValueMapRepository.AddRangeAsync(contactValueMaps);
            await _contactUnitOfWork.SaveChangesAsync();
        }
        public async Task AddContactGroups(IList<ContactGroup> contactGroups)
        {
            await _contactUnitOfWork.GroupContactRepository.AddRangeAsync(contactGroups);
            await _contactUnitOfWork.SaveChangesAsync();
        }
        public async Task UpdateAsync(Contact contact)
        {
            await _contactUnitOfWork.ContactRepository.UpdateAsync(contact);
            await _contactUnitOfWork.SaveChangesAsync();
        }
        public async Task<IList<(int Value, string Text, int Count)>> GetAllGroupsAsync(Guid? userId)
        {
            return (await _groupUnitOfWork.GroupRepository.GetAsync(x => new { Value = x.Id, Text = x.Name, Count = x.ContactGroups.Count },
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.UserId == userId.Value), x => x.OrderBy(o => o.Name), x => x.Include(i => i.ContactGroups), true))
                                                   .Select(x => (Value: x.Value, Text: x.Text, Count: x.Count)).ToList();
        }

        //public async Task<IList<(int Value, string Text, int Count, bool IsChecked)>> GetAllGroupsAsync1(Guid? userId, int contactId)
        //{
        //         return (await _groupUnitOfWork.GroupRepository.GetAsync(x => new { Value = x.Id, Text = x.Name, Count = x.ContactGroups.Count },
        //                                           x => !x.IsDeleted && x.IsActive &&
        //                                           (!userId.HasValue || x.UserId == userId.Value), x => x.OrderBy(o => o.Name), x => x.Include(i => i.ContactGroups), true))
        //                                           .Select(x => (Value: x.Value, Text: x.Text, Count: x.Count)).ToList();

        //}

        public async Task UpdateRangeAsync(IList<ContactValueMap> contactValueMaps)
        {

        }

public async Task<IList<(int Value,string Text)>> GetAllContactValueMaps(Guid? userId)
        {
            return (await _contactUnitOfWork.FieldMapRepository.GetAsync(x => new { Value = x.Id, Text = x.DisplayName },
                                                   x => !x.IsDeleted && x.IsActive && 
                                                   (!userId.HasValue || x.UserId == userId.Value) && x.IsStandard == true && x.DisplayName != "Email", null, null, true))
                                                   .Select(x => (Value: x.Value, Text: x.Text)).ToList();
        }

        public async Task<IList<(int Value, string Text,string Input)>> GetAllContactValueMaps1(Guid? userId,int contactId)
        {
            return (await _contactUnitOfWork.ContactValueMapRepository.GetAsync(x => new { Value = x.Id, Text = x.FieldMap.DisplayName, Input = x.Value },
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.FieldMap.UserId == userId.Value) && x.FieldMap.IsStandard == true && x.FieldMap.DisplayName != "Email" && x.ContactId == contactId, null, null, true))
                                                   .Select(x => (Value: x.Value, Text: x.Text, Input: x.Input)).ToList();
        }

        public async Task<IList<(int Value, string Text)>> GetAllContactValueMapsCustom(Guid? userId)
        {
            return (await _contactUnitOfWork.FieldMapRepository.GetAsync(x => new { Value = x.Id, Text = x.DisplayName },
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.UserId == userId.Value) && x.IsStandard == false, null, null, true))
                                                   .Select(x => (Value: x.Value, Text: x.Text)).ToList();
        }
        public async Task<IList<(int Value, string Text,string Input)>> GetAllContactValueMapsCustom1(Guid? userId, int contactId)
        {
            return (await _contactUnitOfWork.ContactValueMapRepository.GetAsync(x => new { Value = x.Id, Text = x.FieldMap.DisplayName , Input = x.Value },
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.FieldMap.UserId == userId.Value) && x.FieldMap.IsStandard == false && x.ContactId == contactId, null, null, true))
                                                   .Select(x => (Value: x.Value, Text: x.Text ,Input : x.Input)).ToList();
        }

        public async Task<Contact> GetIdByEmail(string email)
        {
            var contact = await _contactUnitOfWork.ContactRepository.GetFirstOrDefaultAsync(x => x, x => x.Email == email,null,true);
            return contact;
        }
        
        public void Dispose()
        {
            _contactUnitOfWork.Dispose();
        }
    }
}
