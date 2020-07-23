using Autofac;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Models.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Models
{
    public class ProfileInformationModel : ProfileBaseModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }


        public ProfileInformationModel() : base() { }
        public ProfileInformationModel(ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor)
        : base(userManager, httpContextAccessor) { }

        internal async void Load()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user != null)
            {
                FullName = user.FullName;
                PhoneNumber = user.PhoneNumber;
                Address = user.Address;
                DateOfBirth = user.DateOfBirth;
                Email = user.Email;
            }
        }



    }
}
