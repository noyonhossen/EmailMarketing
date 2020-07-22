using Autofac;
using EmailMarketing.Membership.Constants;
using EmailMarketing.Membership.Enums;
using EmailMarketing.Membership.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models
{
    public class AdminUsersModel : AdminBaseModel
    {
            private readonly ApplicationUserManager _userManager;
           public AdminUsersModel()
           {
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
           }
        
            public AdminUsersModel(ApplicationUserManager userManager)
            {
                _userManager = userManager;
            }
            public async Task<object> GetAllAsync(DataTablesAjaxRequestModel tableModel)
            {
                var adminCount = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();
                var result = adminCount.Where(x => !x.IsDeleted &&
                    x.Status != EnumApplicationUserStatus.SuperAdmin &&
                    x.UserRoles.Any(ur => ur.Role.Name == ConstantsValue.UserRoleName.Admin)).ToList();

                return new
                {
                    recordsTotal = adminCount.Count(),
                    recordsFiltered = result.Count(),
                    data = (from item in adminCount.ToList()
                            select new string[]
                            {
                                    item.FullName,
                                    item.UserName,
                                    item.Email,
                                    item.PhoneNumber,
                                    item.Id.ToString()
                            }
                            ).ToArray()

                };
            }

        internal async Task<string> DeleteAdminAsync(Guid id)
        {
            var deladmin =await _userManager.FindByIdAsync(id.ToString());
           await _userManager.DeleteAsync(deladmin);
            return (deladmin.UserName);

        }
    }
    }
