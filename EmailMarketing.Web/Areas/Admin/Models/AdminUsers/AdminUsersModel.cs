using Autofac;
using EmailMarketing.Membership.Constants;
using EmailMarketing.Membership.Enums;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models.AdminUsers
{
    public class AdminUsersModel : AdminBaseModel
    {
        private readonly IApplicationUserService _applicationUserService;

        public AdminUsersModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
        }

        public AdminUsersModel(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<object> GetAllAsync(DataTablesAjaxRequestModel tableModel)
        {
            var result = await _applicationUserService.GetAllAdminAsync(
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "FullName", "Email" }),
                tableModel.PageIndex, tableModel.PageSize);

            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,
                data = (from item in result.Items
                        select new string[]
                        {
                                    item.FullName,
                                    item.Email,
                                    item.Gender,
                                    item.PhoneNumber,
                                    item.Id.ToString()
                        }).ToArray()
            };
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var name = await _applicationUserService.DeleteAsync(id);
            return name;
        }
    }
}

