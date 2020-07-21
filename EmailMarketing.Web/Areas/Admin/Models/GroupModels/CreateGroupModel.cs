using EmailMarketing.Framework.Services;
using EmailMarketing.Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models.GroupModels
{
    public class CreateGroupModel:GroupBaseModel
    {
        [Required]
        public string Name { get; set; }        
        
        public CreateGroupModel(IGroupService groupService) : base(groupService) { }
        public CreateGroupModel() : base() { }

        public async Task AddAsync()
        {
            var entity = new Group
            {
                Name = this.Name
            };
            await _groupService.AddAsync(entity);
        }
    }
}
