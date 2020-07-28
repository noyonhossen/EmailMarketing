using Autofac;
using EmailMarketing.Framework.Services;
using EmailMarketing.Membership.Constants;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Enums;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class MemberUserModel :AdminBaseModel
    {
        private readonly AppSettings _userDefaultPassword;
        private readonly ApplicationUserService _applicationUserService;
        private readonly ApplicationUserManager _userManager;
        public MemberUserModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<ApplicationUserService>();
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
            _userDefaultPassword = Startup.AutofacContainer.Resolve<IOptions<AppSettings>>().Value;
        }
   
        public MemberUserModel(ApplicationUserService applicationUserService, ApplicationUserManager userManager)
        {
            _applicationUserService = applicationUserService;
            _userManager = userManager;
        }
        public async Task<object> GetAllAsync(DataTablesAjaxRequestModel tableModel)
        {
            var result = await _applicationUserService.GetAllMemberAsync(
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "User Name","Email" }),
                tableModel.PageIndex, tableModel.PageSize);

            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,
                data = (from item in result.Items
                        select new string[]
                        {
                                    item.UserName,
                                    item.FullName,
                                    item.Email,
                                    item.EmailConfirmed.ToString() == "True" ? "Yes" : "No",
                                    item.PhoneNumber,
                                    item.IsBlocked.ToString() == "True" ? "Yes" : "No",
                                    item.Id.ToString()
                        }
                        ).ToArray()

            };
        }
        public async Task<string> DeleteAsync(Guid id)
        {
            var name = await _applicationUserService.DeleteAsync(id);
            return name;
        }
        public async Task<string> UpdatePasswordHash(Guid id)
        {

            var user = await _applicationUserService.GetByIdAsync(id);
            var newPassword = _userManager.PasswordHasher.HashPassword(user, _userDefaultPassword.UserDefaultPassword);
            var name = await _applicationUserService.ResetPassword(user, newPassword);
            return name;
        }
        public async Task<ApplicationUser> BlockUser(Guid id)
        {

            var user = await _applicationUserService.BlockUnblockAsync(id);
            return user;
        }
    }
}
