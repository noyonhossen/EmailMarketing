using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Entities.Groups;
using EmailMarketing.Framework.Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Contacts
{
    public class EditContactsModel : ContactsBaseModel
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public List<ContactValueTextModel> GroupSelectList { get; set; }
        public List<ContactValueTextModel> ContactValueMaps { get; set; }
        public List<ContactValueTextModel> ContactValueMaps1 { get; set; }
        public List<ContactValueTextModel> ContactValueMapsCustom1 { get; set; }
        public List<ContactValueTextModel> ContactValueMapsCustom { get; set; }

        public Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        public EditContactsModel(IContactService contactService,
            ICurrentUserService currentUserService) : base(contactService, currentUserService)
        {

        }
        public EditContactsModel() : base()
        {

        }

        public async Task LoadContactByIdAsync(int id)
        {
            var temp = new ContactValueTextModel();
            var contact = await _contactService.GetByIdAsync(id);
            this.Id = contact.Id;

            this.Email = contact.Email;

            ContactValueMapsCustom = (await _contactService.GetAllContactValueMapsCustom(_currentUserService.UserId))
                                          .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text }).ToList();

            var contactValueMapsCustom = ContactValueMapsCustom.Select(x => x.Value);

           var ContactValueMapsCustom12 = (await _contactService.GetAllContactValueMapsCustom1(_currentUserService.UserId,contact.Id))
                                           .Select(x => new ContactValueTextModel { Id = x.Id, Value = x.Value, Text = x.Text , Input = x.Input }).ToList();
            
            var contactValueMapsCustom1 = ContactValueMapsCustom12.Select(x => x.Value).ToList();

            foreach (var item in ContactValueMapsCustom)
            {
                if (contactValueMapsCustom1.Contains(item.Value) == false)
                {
                    ContactValueMapsCustom12.Add(
                    new ContactValueTextModel
                    {
                        Id = item.Id,
                        Input = item.Input,
                        Value = item.Value,
                        IsChecked = item.IsChecked,
                        Text = item.Text
                    });
                }
            }
            ContactValueMapsCustom1 = ContactValueMapsCustom12;


            ContactValueMaps = (await _contactService.GetAllContactValueMaps(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text }).ToList();

            var contactValueMaps = ContactValueMaps.Select(x => x.Value);

            var ContactValueMaps12 = (await _contactService.GetAllContactValueMaps1(_currentUserService.UserId, contact.Id))
                                            .Select(x => new ContactValueTextModel { Id = x.Id, Value = x.Value, Text = x.Text, Input = x.Input }).ToList();

            var contactValueMaps1 = ContactValueMaps12.Select(x => x.Value).ToList();

            foreach (var item in ContactValueMaps)
            {
                if (contactValueMaps1.Contains(item.Value) == false)
                {
                    ContactValueMaps12.Add(
                    new ContactValueTextModel
                    {
                        Id = item.Id,
                        Input = item.Input,
                        Value = item.Value,
                        IsChecked = item.IsChecked,
                        Text = item.Text
                    });
                }
            }
            ContactValueMaps1 = ContactValueMaps12;

            var groups = (await _contactService.GetAllGroupsAsync(_currentUserService.UserId))
                                          .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text, Count = x.Count, IsChecked = false }).ToList();

            var isSelectedGroups = new HashSet<int>(contact.ContactGroups.Select(x => x.GroupId));



            List<ContactValueTextModel> GroupList = new List<ContactValueTextModel>();
            
            foreach (var group in groups)
            {
                GroupList.Add(
                    new ContactValueTextModel
                    {
                        IsChecked = isSelectedGroups.Contains(group.Value),
                        Text = group.Text,
                        Count = group.Count,
                        Value = group.Value
                    });
            }

            GroupSelectList = GroupList;

            //ContactValueMapsCustom = (await _contactService.GetAllContactValueMapsCustom(_currentUserService.UserId))
            //                               .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text }).ToList();

            //ContactValueMaps = (await _contactService.GetAllContactValueMaps(_currentUserService.UserId))
            //                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text }).ToList();
            
        }
        public async Task UpdateAsync()
        {
            if (!this.GroupSelectList.Any(x => x.IsChecked)) throw new Exception("Please select at least one group.");

            try
            {
                var newContact = await _contactService.GetByIdAsync(Id);
                newContact.Email = this.Email;
                newContact.UserId = _currentUserService.UserId;
                newContact.CreatedBy = _currentUserService.UserId;
                newContact.Created = DateTime.Now;
                await _contactService.UpdateAsync(newContact);


            var contactValueMaps = new List<ContactValueMap>();
            var newcontactValueMaps = new List<ContactValueMap>();
            foreach (var item in ContactValueMapsCustom1)
            {
                var result = await _contactService.GetContactValueMapByIdAsync(item.Id);
                if (result == null)
                {
                    var contactValueMap = new ContactValueMap();
                    contactValueMap.Value = item.Input;
                    contactValueMap.ContactId = newContact.Id;
                    contactValueMap.FieldMapId = item.Value;
                    newcontactValueMaps.Add(contactValueMap);
                }
                else
                {
                    result.Value = item.Input;
                    result.ContactId = newContact.Id;
                    result.FieldMapId = item.Value;
                    contactValueMaps.Add(result);
                }
            }
            await _contactService.UpdateRangeAsync(contactValueMaps);
            await _contactService.AddContacValueMaps(newcontactValueMaps);

            contactValueMaps.Clear();
            newcontactValueMaps.Clear();

                foreach (var item in ContactValueMaps1)
                {
                    var result = await _contactService.GetContactValueMapByIdAsync(item.Id);
                    if (result == null)
                    {
                        var contactValueMap = new ContactValueMap();
                        contactValueMap.Value = item.Input;
                        contactValueMap.ContactId = newContact.Id;
                        contactValueMap.FieldMapId = item.Value;
                        newcontactValueMaps.Add(contactValueMap);
                    }
                    else
                    {
                        result.Value = item.Input;
                        result.ContactId = newContact.Id;
                        result.FieldMapId = item.Value;
                        contactValueMaps.Add(result);
                    }
                }
                await _contactService.UpdateRangeAsync(contactValueMaps);
                await _contactService.AddContacValueMaps(newcontactValueMaps);


                var contactgroups = (await _contactService.GetAllGroupsAsync(_currentUserService.UserId))
                                          .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text, Count = x.Count, IsChecked = false }).ToList();

            var contactGroups = contactgroups.Select(x => x.Value).ToList();

            var contactCourses = new HashSet<int>
                (newContact.ContactGroups.Select(x => x.GroupId));

            var items = new List<int>();

            foreach (var item in GroupSelectList)
            {
                if (item.IsChecked)
                {
                    items.Add(item.Value);
                }
            }

            foreach (var group in GroupSelectList)
            {
                if (items.Contains(group.Value))
                {
                    if (!contactCourses.Contains(group.Value))
                    {
                        await _contactService.AddContactGroups(new List<ContactGroup> {
                            new ContactGroup { ContactId = newContact.Id, GroupId = group.Value }
                        });
                    }
                }
                else
                {
                    if (contactCourses.Contains(group.Value))
                    {
                        var courseToRemove = newContact.ContactGroups.FirstOrDefault(i => i.GroupId == group.Value);
                        await _contactService.DeleteContactGroupAsync(courseToRemove.Id);
                    }
                }
            }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update Email");
            }


            //await _contactService.UpdateRangeAsync(newContact.ContactValueMaps);

        }
    }
}
