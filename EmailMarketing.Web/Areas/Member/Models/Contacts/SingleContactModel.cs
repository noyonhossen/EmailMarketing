using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Services.Contacts;
using EmailMarketing.Web.Models;
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

        [Required]
        public string Name { get; set; }
        public int GroupId { get; set; }

        //private ICurrentUserService _currentUserService;
        public IList<ValueTextModel> GroupSelectList { get; set; }
        public IList<ValueTextModel> ContactValueMaps { get; set; }
        public IList<ValueTextModel> ContactValueMapsCustom { get; set; }

        public SingleContactModel(IContactService contactService,
           ICurrentUserService currentUserService) : base(contactService, currentUserService)
        {
            _contactService = contactService;
            //_currentUserService = currentUserService; 
             
        }
        public SingleContactModel() : base()
        {
            _contactService = Startup.AutofacContainer.Resolve<IContactService>();
        }

        public async Task<IList<ValueTextModel>> GetAllGroupForSelectAsync()
        {
            return (await _contactService.GetAllGroupsAsync(_currentUserService.UserId))
                                           .Select(x => new ValueTextModel { Value = x.Value , Text = x.Text}).ToList();
        }
        public async Task<IList<ValueTextModel>> GetAllContactValueMaps()
        {
            return (await _contactService.GetAllContactValueMaps(_currentUserService.UserId))
                                           .Select(x => new ValueTextModel { Value = x.Value, Text = x.Text }).ToList();
        }

        public async Task<IList<ValueTextModel>> GetAllContactValueMapsCustom()
        {
            return (await _contactService.GetAllContactValueMapsCustom(_currentUserService.UserId))
                                           .Select(x => new ValueTextModel { Value = x.Value, Text = x.Text }).ToList();
        }

        public async Task SaveContactAsync()
        {
            var newContact = new Contact();
            newContact.CreatedBy = _currentUserService.UserId;
            newContact.Created = DateTime.Now;
            newContact.Email = this.Email;
            await _contactService.AddContact(newContact);
        }

    }
}
