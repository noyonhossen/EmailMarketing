using Autofac;
using EmailMarketing.Web.Areas.Admin.Models;
using EmailMarketing.Web.Areas.Member.Models.Groups;
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
            builder.RegisterType<MemberUserModel>();
            builder.RegisterType<GroupModel>();

            base.Load(builder);
        }
    }
}
