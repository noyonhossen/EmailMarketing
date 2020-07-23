using Autofac;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Models.Profile;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Models
{
    public class UpdateInformationModel : ProfileBaseModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }

        public UpdateInformationModel() : base() { }
        public UpdateInformationModel(ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor)
            : base(userManager, httpContextAccessor) { }

        internal async Task Load()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user != null)
            {
                Id = user.Id;
                FullName = user.FullName;
                PhoneNumber = user.PhoneNumber;
                Address = user.Address;
                DateOfBirth = user.DateOfBirth;
            }
        }
        public async Task UpdateAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user != null)
            {
                user.FullName = FullName;
                user.PhoneNumber = PhoneNumber;
                user.Address = Address;
                user.DateOfBirth = DateOfBirth;
            }
            var result = await _userManager.UpdateAsync(user);
        }
    }
}
