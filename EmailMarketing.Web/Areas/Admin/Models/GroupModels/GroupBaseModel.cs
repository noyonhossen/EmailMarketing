using Autofac;
using EmailMarketing.Framework.Services;
using EmailMarketing.GroupModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Admin.Models.GroupModels
{
    public class GroupBaseModel : AdminBaseModel, IDisposable
    {
        protected readonly IGroupService _groupService;
        public GroupBaseModel(IGroupService groupService)
        {
            _groupService = groupService; 
        }

        public GroupBaseModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }
        public void Dispose()
        {
            _groupService?.Dispose();

        }
    }
}
