using EmailMarketing.GroupModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models.GroupModels
{
    public class GroupModel:GroupBaseModel
    {
        public GroupModel(IGroupService groupService) : base(groupService) { }
        public GroupModel() : base() { }

        internal async Task<object> GetAllAsync(DataTablesAjaxRequestModel tableModel)
        {
            
            var data = await _groupService.GetAllAsync(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Name" })); 

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,                                
                                record.Id.ToString()
                        }
                    ).ToArray()

            };
        }

        internal async Task<string> DeleteAsync(int id)
        {
            var deletedGroup = await _groupService.DeleteAsync(id);
            return deletedGroup.Name;
        }
    }
}
