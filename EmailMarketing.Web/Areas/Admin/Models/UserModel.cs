using EmailMarketing.Framework.Services;
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
    public class UserModel :AdminBaseModel
    {
        private readonly ApplicationUserManager _userManager;
        public UserModel()
        {

        }
        public UserModel(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<object> GetAllAsync(DataTablesAjaxRequestModel tableModel)
        {
            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();
            //var result = query.Where(x => !x.IsDeleted &&
            //    x.Status != EnumApplicationUserStatus.SuperAdmin &&
            //    x.UserRoles.Any(ur => ur.Role.Name == ConstantsValue.UserRoleName.Member)).ToList();


            //var result = await _userService.GetAllAsync(
            //    tableModel.SearchText,
            //    tableModel.GetSortText(new string[] { "UserName", "Email", "EmailConfirmed", "PhoneNumber" }),
            //    tableModel.PageIndex, tableModel.PageSize);

            //return new
            //{
            //    recordsTotal = result.Total,
            //    recordsFiltered = result.TotalFilter,
            //    data = (from item in result.Items
            //            select new string[]
            //            {
            //                        item.UserName,
            //                        item.Email,
            //                        item.EmailConfirmed.ToString(),
            //                        item.PhoneNumber
            //            }
            //            ).ToArray()

            //};
            return new
            {
                recordsTotal = 5,
                recordsFiltered = 10,
                data = (from item in query.ToList()
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
            var result = await _userManager.FindByIdAsync(id.ToString());
            var user = await _userManager.DeleteAsync(result);
            return result.UserName;
        }

    }
}
