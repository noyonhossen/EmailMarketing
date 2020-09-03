using EmailMarketing.Common.Services;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.ProfileModels
{
    public class UpdateInformationModel : MemberBaseModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }

        public UpdateInformationModel() : base() { }
        public UpdateInformationModel(ICurrentUserService currentUserService, IApplicationUserService applicationuserService)
            : base(currentUserService, applicationuserService) { }
        internal async Task Load()
        {
            var user = await _applicationuserService.GetByIdAsync(_currentUserService.UserId);
            if(user!=null)
            {
                //Id = user.Id;
                FullName = user.FullName;
                Email = user.Email;
                DateOfBirth = user.DateOfBirth;
                Gender = user.Gender;
                PhoneNumber = user.PhoneNumber;
                Address = user.Address;
            }
        }
        internal void GetModelData(ApplicationUser user)
        {
            user.FullName = FullName;
            user.Email = Email;
            user.DateOfBirth = DateOfBirth;
            user.Gender = Gender;
            user.PhoneNumber = PhoneNumber; 
            user.Address = Address;
        }
        internal async Task UpdateMemberAsync()
        {

            var user = await _applicationuserService.GetByIdAsync(_currentUserService.UserId);
            GetModelData(user);
            await _applicationuserService.UpdateAsync(user);
        }
    }
}
