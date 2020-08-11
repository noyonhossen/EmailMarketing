using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Contacts
{
    public class ContactDetailsModel : ContactsBaseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public ContactDetailsModel() : base()
        {
                
        }

        public async Task LoadByIdAsync(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            this.Id = contact.Id;
            this.Email = contact.Email;
        }
    }
}
