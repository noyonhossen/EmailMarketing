using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Contacts
{
    public class ViewContactUploadModel:ContactsBaseModel
    {
        public int UserId { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public bool IsSucceed { get; set; }
        public bool IsUpdateExisting { get; set; }
        public bool HasColumnHeader { get; set; }
        public bool IsSendEmailNotify { get; set; }
        public string SendEmailAddress { get; set; }
        public int SucceedEntryCount { get; set; }
        public bool IsProcessing { get; set; }

        public ViewContactUploadModel(IContactUploadService contactUploadService,
           ICurrentUserService currentUserService) : base(contactUploadService, currentUserService)
        {

        }

        public ViewContactUploadModel() : base()
        {

        }

        public async Task GetContactUploadData(int id)
        {

            var result = await _contactUploadService.GetByIdAsync(id);
            this.FileName = result.FileName;
            this.FileUrl = result.FileUrl;
            this.SucceedEntryCount = result.SucceedEntryCount;
            this.IsSucceed = result.IsSucceed;
            this.IsProcessing = result.IsProcessing;



        }
    }
}
