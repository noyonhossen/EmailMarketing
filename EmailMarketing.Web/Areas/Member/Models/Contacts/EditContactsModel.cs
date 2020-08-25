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
        public List<ContactValueTextModel> ContactValueMapsSelected { get; set; }
        public List<ContactValueTextModel> ContactValueMapsCustomSelected { get; set; }
        public List<ContactValueTextModel> ContactValueMapsCustom { get; set; }

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

            var ContactValueMapsCustomSelected = (await _contactService.GetAllContactValueMapsCustom(_currentUserService.UserId, contact.Id))
                                           .Select(x => new ContactValueTextModel { Id = x.Id, Value = x.Value, Text = x.Text, Input = x.Input }).ToList();

            var contactValueMapsCustomSelected = ContactValueMapsCustomSelected.Select(x => x.Value).ToList();

            foreach (var item in ContactValueMapsCustom)
            {
                if (contactValueMapsCustomSelected.Contains(item.Value) == false)
                {
                    ContactValueMapsCustomSelected.Add(
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
            this.ContactValueMapsCustomSelected = ContactValueMapsCustomSelected;


            ContactValueMaps = (await _contactService.GetAllContactValueMaps(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text }).ToList();


            var ContactValueMapsSelected = (await _contactService.GetAllContactValueMaps(_currentUserService.UserId, contact.Id))
                                            .Select(x => new ContactValueTextModel { Id = x.Id, Value = x.Value, Text = x.Text, Input = x.Input }).ToList();

            var contactValueMapsSelected = ContactValueMapsSelected.Select(x => x.Value).ToList();

            foreach (var item in ContactValueMaps)
            {
                if (contactValueMapsSelected.Contains(item.Value) == false)
                {
                    ContactValueMapsSelected.Add(
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
            this.ContactValueMapsSelected = ContactValueMapsSelected;

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
        }

        public async Task UpdateAsync()
        {
            if (!this.GroupSelectList.Any(x => x.IsChecked)) throw new Exception("Please select at least one group.");

            try
            {
                var updateContact = new Contact
                {
                    Email = this.Email, 
                    Id = this.Id,
                    UserId = _currentUserService.UserId,
                    CreatedBy = _currentUserService.UserId,
                    Created = DateTime.Now
                };
                await _contactService.UpdateAsync(updateContact);

                var contactValueMaps = new List<ContactValueMap>();
                var newcontactValueMaps = new List<ContactValueMap>();
                foreach (var item in ContactValueMapsCustomSelected)
                {
                    var result = await _contactService.GetContactValueMapByIdAsync(item.Id);
                    if (result == null)
                    {
                        var contactValueMap = new ContactValueMap();
                        contactValueMap.Value = item.Input;
                        contactValueMap.ContactId = this.Id;
                        contactValueMap.FieldMapId = item.Value;
                        newcontactValueMaps.Add(contactValueMap);
                    }
                    else
                    {
                        result.Value = item.Input;
                        result.ContactId = this.Id;
                        result.FieldMapId = item.Value;
                        contactValueMaps.Add(result);
                    }
                }
                //await _contactService.UpdateRangeAsync(contactValueMaps);
                if (newcontactValueMaps.Count() > 0)
                    await _contactService.AddContacValueMaps(newcontactValueMaps);

                contactValueMaps.Clear();
                newcontactValueMaps.Clear();

                foreach (var item in ContactValueMapsSelected)
                {
                    var result = await _contactService.GetContactValueMapByIdAsync(item.Id);
                    if (result == null)
                    {
                        var contactValueMap = new ContactValueMap();
                        contactValueMap.Value = item.Input;
                        contactValueMap.ContactId = this.Id;
                        contactValueMap.FieldMapId = item.Value;
                        newcontactValueMaps.Add(contactValueMap);
                    }
                    else
                    {
                        result.Value = item.Input;
                        result.ContactId = this.Id;
                        result.FieldMapId = item.Value;
                        contactValueMaps.Add(result);
                    }
                }
                //await _contactService.UpdateRangeAsync(contactValueMaps);
                if(newcontactValueMaps.Count()>0)
                    await _contactService.AddContacValueMaps(newcontactValueMaps);


                var contactgroups = (await _contactService.GetAllGroupsAsync(_currentUserService.UserId))
                                              .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text, Count = x.Count, IsChecked = false }).ToList();


                var contact = await _contactService.GetByIdAsync(this.Id);
                var contactGroups = new HashSet<int>
                    (contact.ContactGroups.Select(x => x.GroupId));

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
                        if (!contactGroups.Contains(group.Value))
                        {
                            await _contactService.AddContactGroups(new List<ContactGroup> {
                            new ContactGroup { ContactId = this.Id, GroupId = group.Value }
                        });
                        }
                    }
                    else
                    {
                        if (contactGroups.Contains(group.Value))
                        {
                            await _contactService.DeleteContactGroupAsync(group.Value, contact.Id);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update Email");
            }

        }
    }
}
