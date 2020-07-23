using Autofac;
using EmailMarketing.Web.Areas.Admin.Models;
using EmailMarketing.Web.Areas.Admin.Models.GroupModels;
using EmailMarketing.Web.Models;
using EmailMarketing.Web.Models.Profile;
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
            builder.RegisterType<GroupModel>();
            builder.RegisterType<AdminUsersModel>();
            //builder.RegisterType<ProfileBaseModel>();
            builder.RegisterType<ProfileInformationModel>();
            base.Load(builder);
        }
    }
}
