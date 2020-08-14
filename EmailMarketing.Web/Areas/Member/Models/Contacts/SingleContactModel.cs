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
    public class SingleContactModel : ContactsBaseModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Required]
        public IList<ContactValueTextModel> GroupSelectList { get; set; }
        public IList<ContactValueTextModel> ContactValueMaps { get; set; }
        public IList<ContactValueTextModel> ContactValueMapsCustom { get; set; }

        public SingleContactModel(IContactService contactService,
           ICurrentUserService currentUserService) : base(contactService, currentUserService)
        {
            _contactService = contactService;
            
        }
        public SingleContactModel() : base()
        {
            _contactService = Startup.AutofacContainer.Resolve<IContactService>();
        }

        public async Task<IList<ContactValueTextModel>> GetAllGroupForSelectAsync()
        {
            return (await _contactService.GetAllGroupsAsync(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value , Text = x.Text, Count = x.Count, IsChecked = false}).ToList();
        }
        public async Task<IList<ContactValueTextModel>> GetAllContactValueMaps()
        {
            return (await _contactService.GetAllContactValueMaps(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text}).ToList();
        }

        public async Task<IList<ContactValueTextModel>> GetAllContactValueMapsCustom()
        {
            return (await _contactService.GetAllContactValueMapsCustom(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text }).ToList();
        }


        public async Task SaveContactAsync()
        {

            var newContact = new Contact();
            newContact.CreatedBy = _currentUserService.UserId;
            newContact.Created = DateTime.Now;
            newContact.Email = this.Email;
            newContact.UserId = _currentUserService.UserId;
            var newcontactId = await _contactService.GetIdByEmail(Email);

            if (newcontactId != -1)
            {
                newContact.Id = newcontactId;
                await _contactService.UpdateAsync(newContact);
            }
            else
            {
                await _contactService.AddContact(newContact);
                var contactId = await _contactService.GetIdByEmail(Email);

                var contactValueMaps = new List<ContactValueMap>();
                foreach (var item in ContactValueMaps)
                {
                    var contactValueMap = new ContactValueMap();
                    contactValueMap.ContactId = contactId;
                    contactValueMap.Value = item.Input;
                    contactValueMap.FieldMapId = (int)item.Value;
                    contactValueMaps.Add(contactValueMap);
                }
                await _contactService.AddContacValueMaps(contactValueMaps);

                var contactValueMapCustoms = new List<ContactValueMap>();
                foreach (var item in ContactValueMapsCustom)
                {
                    var contactValueMap = new ContactValueMap();
                    contactValueMap.ContactId = contactId;
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
                        newContactGroup.ContactId = contactId;
                        newContactGroup.GroupId = (int)item.Value;
                        contactGroups.Add(newContactGroup);
                    }
                }
                await _contactService.AddContactGroups(contactGroups);
            }
        }

    }
}
