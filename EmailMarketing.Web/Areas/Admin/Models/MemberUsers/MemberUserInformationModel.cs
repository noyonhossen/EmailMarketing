using Autofac;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class MemberUserInformationModel : AdminBaseModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string LastPassword { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBlocked { get; set; }

        private readonly ApplicationUserService _applicationUserService;

        public MemberUserInformationModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<ApplicationUserService>();
        }
        public MemberUserInformationModel(ApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }
        public async Task LoadByIdAsync(Guid id)
        {
            var user = await _applicationUserService.GetByIdAsync(id);
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.EmailConfirmed = user.EmailConfirmed;
            this.PhoneNumber = user.PhoneNumber;
            this.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            this.FullName = user.FullName;
            this.Address = user.Address;
            this.Gender = user.Gender;
            this.DateOfBirth = user.DateOfBirth.ToString();
            this.IsActive = user.IsActive;
            this.IsDeleted = user.IsDeleted;
            this.IsBlocked = user.IsBlocked;

        }
    }
}
