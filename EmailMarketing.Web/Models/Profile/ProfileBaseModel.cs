using Autofac;
using EmailMarketing.Membership.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Models.Profile
{
    public class ProfileBaseModel
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ApplicationUserManager _userManager;
        public ProfileBaseModel()
        {
            _userManager = Startup.AutofacContainer.Resolve<ApplicationUserManager>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public ProfileBaseModel(ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor)
        {

            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
