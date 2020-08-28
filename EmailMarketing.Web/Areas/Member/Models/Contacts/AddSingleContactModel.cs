using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Services.Contacts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Contacts
{
    public class AddSingleContactModel : ContactsBaseModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool IsContactExist { get; set; }
        public IList<ContactValueTextModel> GroupSelectList { get; set; }
        public IList<ContactValueTextModel> ContactValueMaps { get; set; }
        public IList<ContactValueTextModel> ContactValueMapsCustom { get; set; }

        public AddSingleContactModel(IContactService contactService,
           ICurrentUserService currentUserService) : base(contactService, currentUserService)
        {

        }
        public AddSingleContactModel() : base()
        {

        }

        public async Task<IList<ContactValueTextModel>> GetAllGroupForSelectAsync()
        {
            return (await _contactService.GetAllGroupsAsync(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text, Count = x.Count, IsChecked = false }).ToList();
        }
        public async Task<IList<ContactValueTextModel>> GetAllContactValueMaps()
        {
            return (await _contactService.GetAllContactValueMapsStandard())
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text }).ToList();
        }

        public async Task<IList<ContactValueTextModel>> GetAllContactValueMapsCustom()
        {
            return (await _contactService.GetAllContactValueMapsCustom(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text }).ToList();
        }

        public async Task<Contact> IsContactExistAsync()
        {
            var existingContact = await _contactService.GetIdByEmail(Email);
            IsContactExist = true;
            return existingContact;
        }

        public async Task SaveContactAsync()
        {
            if (!this.GroupSelectList.Any(x => x.IsChecked)) throw new Exception("Please select at least one group.");
            else
            {
                try
                {
                    var newContact = new Contact();
                    newContact.CreatedBy = _currentUserService.UserId;
                    newContact.Created = DateTime.Now;
                    newContact.Email = this.Email;
                    newContact.UserId = _currentUserService.UserId;

                    await _contactService.AddContact(newContact);

                    var contactValueMaps = new List<ContactValueMap>();
                    foreach (var item in ContactValueMaps)
                    {
                        var contactValueMap = new ContactValueMap();
                        contactValueMap.ContactId = newContact.Id;
                        contactValueMap.Value = item.Input;
                        contactValueMap.FieldMapId = (int)item.Value;
                        contactValueMaps.Add(contactValueMap);
                    }
                    await _contactService.AddContacValueMaps(contactValueMaps);

                    var contactValueMapCustoms = new List<ContactValueMap>();
                    foreach (var item in ContactValueMapsCustom)
                    {
                        var contactValueMap = new ContactValueMap();
                        contactValueMap.ContactId = newContact.Id;
                        contactValueMap.Value = item.Input;
                        contactValueMap.FieldMapId = (int)item.Value;
                        contactValueMapCustoms.Add(contactValueMap);
                    }
                    await _contactService.AddContacValueMaps(contactValueMapCustoms);

                    var contactGroups = new List<ContactGroup>();
                    foreach (var item in GroupSelectList)
                    {
                        if (item.IsChecked)
                        {
                            var newContactGroup = new ContactGroup();
                            newContactGroup.ContactId = newContact.Id;
                            newContactGroup.GroupId = (int)item.Value;
                            contactGroups.Add(newContactGroup);
                        }
                    }
                    await _contactService.AddContactGroups(contactGroups);

                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to Add Contact.");
                }
            }
        }

    }
}
