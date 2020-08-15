using Autofac;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Entities.Groups;
using EmailMarketing.Framework.Services.Groups;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Areas.Member.Models.Groups;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Groups
{
    public class CreateGroupModel:GroupBaseModel
    {
        
        [Required]
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public CreateGroupModel(IGroupService groupService, IApplicationUserService applicationUserService,
            ICurrentUserService currentUserService) : base(groupService, applicationUserService, currentUserService) 
        {
            
        }
        public CreateGroupModel() : base() 
        {
            
        }
        
        public async Task AddAsync()
        {
            
            var entity = new Group
            {
                Name = this.Name,
                UserId = _currentUserService.UserId

            };
            await _groupService.AddAsync(entity);
        }
    }
}
