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
    public class UserModel :AdminBaseModel
    {
        private readonly ApplicationUserManager _userManager;

        public UserModel()
        {
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
        }
   
        public UserModel(ApplicationUserManager userManager)
        {
            _userManager = userManager;
            
        }
        public async Task<object> GetAllAsync(DataTablesAjaxRequestModel tableModel)
        {
            var query =  _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();
            var users = query.Where(x => !x.IsDeleted &&
                x.Status != EnumApplicationUserStatus.SuperAdmin &&
                x.UserRoles.Any(ur => ur.Role.Name == ConstantsValue.UserRoleName.Member)).ToList();

            return new
            {
                recordsTotal = query.Count(),
                recordsFiltered = users.Count(),
                data = (from item in users.ToList()
                        select new string[]
                        {
                                    item.UserName,
                                    item.Email,
                                    item.EmailConfirmed.ToString(),
                                    item.Id.ToString()
                        }
                        ).ToArray()

            };
        }
        public async Task<string> DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = await _userManager.DeleteAsync(user);
            return user.UserName;
        }
        

    }
}
