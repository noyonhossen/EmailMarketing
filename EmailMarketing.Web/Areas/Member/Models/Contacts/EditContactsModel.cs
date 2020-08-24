using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities.Contacts;
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
        //[Required]
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
          
            ContactValueMapsCustom1 = (await _contactService.GetAllContactValueMapsCustom1(_currentUserService.UserId,contact.Id))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text , Input = x.Input }).ToList();

            ContactValueMaps1 = (await _contactService.GetAllContactValueMaps1(_currentUserService.UserId, contact.Id))
                                         .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text, Input = x.Input }).ToList();


            
            GroupSelectList = contact.ContactGroups.Select(x => new ContactValueTextModel { Value = x.GroupId , IsChecked = true , Text = x.Group.Name }).ToList();

            ContactValueMapsCustom = (await _contactService.GetAllContactValueMapsCustom(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text }).ToList();

            ContactValueMaps = (await _contactService.GetAllContactValueMaps(_currentUserService.UserId))
                                       .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text }).ToList();
            
            
        }
        public async Task UpdateAsync()
        {

            var newContact = new Contact();
            newContact.Email = this.Email;
            newContact.Id = this.Id;
            await _contactService.UpdateAsync(newContact);




        }
    }
}
