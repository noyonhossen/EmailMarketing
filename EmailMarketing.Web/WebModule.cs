using Autofac;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Areas.Admin.Models;
using EmailMarketing.Web.Areas.Admin.Models.AdminModels;
using EmailMarketing.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Web
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public WebModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExpenseModel>();
            builder.RegisterType<AdminUsersModel>();
            builder.RegisterType<ApplicationUserService>();
            builder.RegisterType<MemberUserModel>();
            builder.RegisterType<ApplicationUserService>();
            builder.RegisterType<ChangeDefaultPasswordViewModel>();

            base.Load(builder);
        }
    }
}  
