using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.Groups;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Areas.Admin.Models;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Groups
{
    public class GroupModel:GroupBaseModel
    {
        public GroupModel(IGroupService groupService, IApplicationUserService applicationUserService,
            ICurrentUserService currentUserService) : base(groupService, applicationUserService,currentUserService) { }
        public GroupModel() : base() { }

        public async Task<object> GetAllAsync(DataTablesAjaxRequestModel tableModel)
        {
            var result = await _groupService.GetAllAsync(
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Name" }),
                tableModel.PageIndex, tableModel.PageSize);
            var userId = _currentUserService.UserId;
            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,
                
                data = (from item in result.Items where(item.UserId==userId)
                        select new string[]
                        {
                                    item.Name,
                                    item.Id.ToString()
                        }
                        ).ToArray()

            };
        }

        public async Task<string> DeleteAsync(int id)
        {
            var group = await _groupService.DeleteAsync(id);
            return group.Name;
        }
    }
}
