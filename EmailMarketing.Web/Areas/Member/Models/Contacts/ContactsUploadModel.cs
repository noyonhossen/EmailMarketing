using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.Contacts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Contacts
{
    public class ContactsUploadModel : ContactsBaseModel 
    {
        public IFormFile ContactFile { get; set; } 
        public bool HasColumnHeader { get; set; }

        public ContactsUploadModel(IContactExcelService contactExcelService,
            ICurrentUserService currentUserService) : base(contactExcelService, currentUserService)
        {

        }
        public ContactsUploadModel() : base()
        {

        }

        public async Task<object> GetAllFieldMapForSelectAsync()
        {
            return (await _contactExcelService.GetAllFieldMapForSelectAsync(_currentUserService.UserId)).GroupBy(x => x.IsStandard)
                                .Select(x => new { IsStandard = x.Key, Values = x.Select(y => new { y.Value, y.Text }).ToList() })
                                .OrderBy(x => x.IsStandard).ToList();
        }
    }
}
