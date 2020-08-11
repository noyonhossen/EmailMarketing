using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Contacts
{
    public class ContactsModel : ContactsBaseModel
    {
        public IList<Contact> Contacts { get; set; }

        public ContactsModel(IContactService contactService,
            ICurrentUserService currentUserService) : base(contactService, currentUserService)
        {

        }
        public ContactsModel() : base()
        {

        }

        //public async Task<IList<Contact>> GetAllContactAsync()
        //{
        //    return (await _contactExcelService.GetAllContactsAsync(_currentUserService.UserId));
        //}
        public async Task<object> GetAllContactAsync(DataTablesAjaxRequestModel tableModel)
        {
            var result = await _contactService.GetAllContactAsync(
                _currentUserService.UserId,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Email" }),
                tableModel.PageIndex, tableModel.PageSize);

           
            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,
                data = (from item in result.Items
                        select new string[]
                        {
                            item.Email,
                            item.Email,
                            item.Id.ToString()
                        }).ToArray()

            };
        }
    }
}
