using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.Services.Contacts;
using EmailMarketing.Framework.Services.Groups;
using EmailMarketing.Web.Core;
using EmailMarketing.Web.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Contacts
{
    public class ContactsUploadModel : ContactsBaseModel 
    {
        [Required]
        public IFormFile ContactFile { get; set; }
        [Required]
        public int GroupId { get; set; }
        public bool IsUpdateExisting { get; set; }
        public bool HasColumnHeader { get; set; }
        public bool IsSendEmailNotify { get; set; }
        public string SendEmailAddress { get; set; }
        public IList<int> ContactUploadFieldMaps { get; set; }

        public IList<ContactValueTextModel> GroupSelectList { get; set; }

        private readonly IFileStorage _fileStorage;
        private readonly IGroupService _groupService;

        public ContactsUploadModel(IContactExcelService contactExcelService, IGroupService groupService, IFileStorage fileStorage,
            ICurrentUserService currentUserService) : base(contactExcelService, currentUserService)
        {
            this._fileStorage = fileStorage;
            _groupService = groupService;
        }
        public ContactsUploadModel() : base()
        {
            _fileStorage = Startup.AutofacContainer.Resolve<IFileStorage>();
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }

        public async Task SaveContactsUploadAsync()
        {
            if (this.GroupId == 0) throw new Exception("Please select group.");
            if (!(await this._contactExcelService.IsSelectedEmailFieldMap(this.ContactUploadFieldMaps))) throw new Exception("Please select at least email field map.");

            #region file save
            if (this.ContactFile == null || this.ContactFile.Length <= 0) throw new Exception("Please select file");

            var fileUrl = "C:\\EmailMarketingTeamA\\ContactsFiles";
            var url = await _fileStorage.StoreFileAsync(fileUrl, this.ContactFile);
            fileUrl = Path.Combine(fileUrl, url);
            #endregion

            var entity = new ContactUpload();
            entity.FileUrl = fileUrl;
            //entity.GroupId = this.GroupId;
            entity.HasColumnHeader = this.HasColumnHeader;
            entity.IsUpdateExisting = this.IsUpdateExisting;
            entity.IsSendEmailNotify = this.IsSendEmailNotify;
            entity.SendEmailAddress = this.SendEmailAddress;
            entity.ContactUploadFieldMaps = new List<ContactUploadFieldMap>();

            for (int i = 0; i < this.ContactUploadFieldMaps.Count; i++)
            {
                if(this.ContactUploadFieldMaps[i] != -1)
                {
                    var conFieldMap = new ContactUploadFieldMap();
                    conFieldMap.FieldMapId = this.ContactUploadFieldMaps[i];
                    conFieldMap.Index = i;

                    entity.ContactUploadFieldMaps.Add(conFieldMap);
                }    
            }

            await _contactExcelService.AddContactUploadAsync(entity);
        }

        public async Task<object> GetAllFieldMapForSelectAsync()
        {
            return (await _contactExcelService.GetAllFieldMapForSelectAsync(_currentUserService.UserId)).GroupBy(x => x.IsStandard)
                                .Select(x => new { IsStandard = x.Key, Values = x.Select(y => new { y.Value, y.Text }).ToList() })
                                .OrderByDescending(x => x.IsStandard).ToList();
        }

        public async Task<IList<ContactValueTextModel>> GetAllGroupForSelectAsync()
        {
            return (await _groupService.GetAllGroupForSelectAsync(_currentUserService.UserId))
                                           .Select(x => new ContactValueTextModel { Value = x.Value, Text = x.Text }).ToList();
        }
    }
}
