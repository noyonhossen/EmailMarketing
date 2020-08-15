using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Groups;
using EmailMarketing.Framework.Services.Groups;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Groups
{
    public class EditGroupModel:GroupBaseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public EditGroupModel(IGroupService groupService, IApplicationUserService applicationUserService,
            ICurrentUserService currentUserService) : base(groupService, applicationUserService, currentUserService)
        {
            
        }
        public EditGroupModel() : base()
        {

        }

        public async Task LoadByIdAsync(int id)
        {
            var result = await _groupService.GetByIdAsync(id);
            this.Id = result.Id;
            this.Name = result.Name;
        }

        public async Task UpdateAsync()
        {
            var entity = new Group
            {
                Id = this.Id,
                Name = this.Name,
            };
            await _groupService.UpdateAsync(entity);
        }
    }
}
