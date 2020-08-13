using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Services.Groups;
using EmailMarketing.Framework.UnitOfWork;
using EmailMarketing.Framework.UnitOfWork.Contacts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IList<(int Value, string Text,int Count)>> GetAllGroupsAsync(Guid? userId)
        {
            return (await _groupUnitOfWork.GroupRepository.GetAsync(x => new { Value = x.Id, Text = x.Name , Count = x.ContactGroups.Count() },
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.UserId == userId.Value), x => x.OrderBy(o => o.Name), null, true))
                                                   .Select(x => (Value: x.Value, Text: x.Text , Count: x.Count)).ToList();
        }
        public async Task<IList<(int Value,string Text)>> GetAllContactValueMaps(Guid? userId)
        {
            return (await _contactUnitOfWork.FieldMapRepository.GetAsync(x => new { Value = x.Id, Text = x.DisplayName },
                                                   x => !x.IsDeleted && x.IsActive && 
                                                   (!userId.HasValue || x.UserId == userId.Value) && x.IsStandard == true, null, null, true))
                                                   .Select(x => (Value: x.Value, Text: x.Text)).ToList();
        }

        public async Task<IList<(int Value, string Text)>> GetAllContactValueMapsCustom(Guid? userId)
        {
            return (await _contactUnitOfWork.FieldMapRepository.GetAsync(x => new { Value = x.Id, Text = x.DisplayName },
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.UserId == userId.Value) && x.IsStandard == false, null, null, true))
                                                   .Select(x => (Value: x.Value, Text: x.Text)).ToList();
        }

        public async Task<int> GetIdByEmail(string email)
        {
            var contact = await _contactUnitOfWork.ContactRepository.GetFirstOrDefaultAsync(x => x, x => x.Email == email,null,true);
            return (contact == null?-1:contact.Id);
        }
        
        public void Dispose()
        {
            _contactUnitOfWork.Dispose();
        }
    }
}
