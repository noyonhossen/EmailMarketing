using EmailMarketing.Common.Exceptions;
using EmailMarketing.Common.Extensions;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Contacts;
using EmailMarketing.Framework.UnitOfWork.Contacts;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Services.Contacts
{
    public class ContactExcelService : IContactExcelService
    {
        private IContactExcelUnitOfWork _contactExcelUnitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public ContactExcelService(IContactExcelUnitOfWork contactExcelUnitOfWork, ICurrentUserService currentUserService, IDateTime dateTime)
        {
            _contactExcelUnitOfWork = contactExcelUnitOfWork;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public async Task<(int SucceedCount, int ExistCount, int InvalidCount)> ContactExcelImportAsync(ContactUpload contactUpload)
        {
            if (string.IsNullOrWhiteSpace(contactUpload.FileUrl) || !File.Exists(contactUpload.FileUrl)) throw new Exception("File not found.");

            var newContacts = new List<Contact>();
            var existingContacts = new List<Contact>();
            var isFirstRowHeader = true;
            var existCount = 0;
            var invalidCount = 0;
            var emailIndex = contactUpload.ContactUploadFieldMaps.FirstOrDefault(x => x.FieldMap.DisplayName == "Email")?.Index;

            if(!emailIndex.HasValue) throw new Exception("Email column not found.");

            using (var stream = File.Open(contactUpload.FileUrl, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            if (isFirstRowHeader && contactUpload.HasColumnHeader)
                            {
                                isFirstRowHeader = false;
                                continue;
                            }

                            var newContact = new Contact();
                            newContact.ContactValueMaps = new List<ContactValueMap>();
                            //newContact.GroupId = contactUpload.GroupId;
                            newContact.ContactUploadId = contactUpload.Id;
                            newContact.Email = reader.GetString(emailIndex.Value);
                            newContact.Created = _dateTime.Now;
                            newContact.CreatedBy = _currentUserService.UserId;

                            for (int i = 0; i < contactUpload.ContactUploadFieldMaps.Count; i++)
                            {
                                var fileIndex = contactUpload.ContactUploadFieldMaps[i].Index;
                                if (fileIndex == emailIndex) continue;
                                var contactValMap = new ContactValueMap();
                                contactValMap.FieldMapId = contactUpload.ContactUploadFieldMaps[i].FieldMapId;
                                contactValMap.Value = reader.GetValue(fileIndex).ParseObjectToString();
                                newContact.ContactValueMaps.Add(contactValMap);
                            }

                            if (string.IsNullOrWhiteSpace(newContact.Email))
                            {
                                invalidCount++;
                                continue;
                            }

                            #region Existing Contact Update
                            if (contactUpload.IsUpdateExisting)
                            {
                                //var existingContact = await _contactExcelUnitOfWork.ContactRepository.GetFirstOrDefaultAsync(x => x, 
                                //                                x => x.GroupId == newContact.GroupId && x.Email.ToLower() == newContact.Email, 
                                //                                null, true);

                                var existingContact = (Contact?)null;

                                if (existingContact != null)
                                {
                                    existingContact.ContactValueMaps = new List<ContactValueMap>();
                                    var newContactValMaps = new List<ContactValueMap>();

                                    for (int i = 0; i < contactUpload.ContactUploadFieldMaps.Count; i++)
                                    {
                                        var fileIndex = contactUpload.ContactUploadFieldMaps[i].Index;
                                        if (fileIndex == emailIndex) continue;
                                        var contactValMap = new ContactValueMap();
                                        contactValMap.ContactId = existingContact.Id;
                                        contactValMap.FieldMapId = contactUpload.ContactUploadFieldMaps[i].FieldMapId;
                                        contactValMap.Value = reader.GetValue(fileIndex).ParseObjectToString();
                                        //existingContact.ContactValueMaps.Add(contactValMap);
                                        newContactValMaps.Add(contactValMap);
                                    }

                                    #region Contact Value Maps Update
                                    var existingContactValueMaps = await _contactExcelUnitOfWork.ContactValueMapRepository.GetAsync(x => x,
                                                                            x => x.ContactId == existingContact.Id, null, null, true);
                                    if(existingContactValueMaps.Any())
                                        await _contactExcelUnitOfWork.ContactValueMapRepository.DeleteRangeAsync(existingContactValueMaps);
                                    if (newContactValMaps.Any())
                                        await _contactExcelUnitOfWork.ContactValueMapRepository.AddRangeAsync(newContactValMaps);
                                    await _contactExcelUnitOfWork.SaveChangesAsync();
                                    #endregion

                                    existingContact.ContactUploadId = contactUpload.Id;
                                    existingContact.LastModified = _dateTime.Now;
                                    existingContact.LastModifiedBy = _currentUserService.UserId;

                                    existingContacts.Add(existingContact);

                                    existCount++;
                                    continue;
                                }
                            }
                            #endregion

                            newContacts.Add(newContact);
                        }
                    } while (reader.NextResult());
                }
            }

            if(newContacts.Any())
                await _contactExcelUnitOfWork.ContactRepository.AddRangeAsync(newContacts);
            if(existingContacts.Any())
                await _contactExcelUnitOfWork.ContactRepository.UpdateRangeAsync(existingContacts);
            await _contactExcelUnitOfWork.SaveChangesAsync();

            #region Contact Upload Update
            var existingContactUpload = await _contactExcelUnitOfWork.ContactUploadRepository.GetFirstOrDefaultAsync(x => x, 
                                                x => x.Id == contactUpload.Id, null, true);
            existingContactUpload.IsSucceed = true;
            existingContactUpload.IsProcessing = false;
            existingContactUpload.SucceedEntryCount = newContacts.Count;
            await _contactExcelUnitOfWork.ContactUploadRepository.UpdateAsync(existingContactUpload);
            await _contactExcelUnitOfWork.SaveChangesAsync();
            #endregion

            #region Succeed Email Sending
            if(contactUpload.IsSendEmailNotify)
            {
                var emailAddress = contactUpload.SendEmailAddress.Split(',').ToList();
                // send email
            }
            #endregion

            return (newContacts.Count, existCount, invalidCount);
        }

        public async Task<(int SucceedCount, int ExistCount, int InvalidCount)> ContactExcelImportAsync(int contactUploadId)
        {
            var contactUpload = await _contactExcelUnitOfWork.ContactUploadRepository.GetFirstOrDefaultAsync(x => x, x => x.Id == contactUploadId, 
                                    x => x.Include(i => i.ContactUploadFieldMaps).ThenInclude(i => i.FieldMap), true);

            var result = await this.ContactExcelImportAsync(contactUpload);

            return (result.SucceedCount, result.ExistCount, result.InvalidCount);
        }

        public async Task<IList<(int Value, string Text, bool IsStandard)>> GetAllFieldMapForSelectAsync(Guid? userId)
        {
            return (await _contactExcelUnitOfWork.FieldMapRepository.GetAsync(x => new { Value= x.Id, Text= x.DisplayName, IsStandard= x.IsStandard }, 
                                                    x => !x.IsDeleted && x.IsActive &&
                                                    (x.IsStandard || (!userId.HasValue || x.UserId == userId.Value)), x => x.OrderBy(o => o.DisplayName), null, true))
                                                    .Select(x => (Value: x.Value, Text: x.Text, IsStandard: x.IsStandard)).ToList();
        }
        
        public async Task<IList<ContactUpload>> GetUploadedContact()
        {
            var result = await _contactExcelUnitOfWork.ContactUploadRepository.GetAsync(x => x, x => x.IsProcessing == true, null, null, true);
            return result;
        }

        public async Task AddContactUploadAsync(ContactUpload entity)
        {
            entity.Created = _dateTime.Now;
            entity.CreatedBy = _currentUserService.UserId;
            
            await _contactExcelUnitOfWork.ContactUploadRepository.AddAsync(entity);
            await _contactExcelUnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsSelectedEmailFieldMap(IList<int> values)
        {
            return await _contactExcelUnitOfWork.FieldMapRepository.IsExistsAsync(x => values.Contains(x.Id) && x.DisplayName == "Email");
        }

        public async Task<IList<Contact>> GetAllContactsAsync(Guid? userId)
        {
            //return await _contactExcelUnitOfWork.ContactRepository.GetAsync(x => x,
            //     x => !x.IsDeleted && x.IsActive && (!userId.HasValue || x.Group.UserId == userId.Value),
            //     x => x.OrderByDescending(o => o.Created), 
            //     x => x.Include(o => o.Group).Include(o => o.ContactValueMaps).ThenInclude(o => o.FieldMap),
            //     true);

            return null;
        }

        public void Dispose()
        {
            _contactExcelUnitOfWork?.Dispose();
        }
    }
}
