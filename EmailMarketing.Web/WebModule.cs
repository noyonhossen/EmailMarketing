using Autofac;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Areas.Admin.Models;
using EmailMarketing.Web.Areas.Member.Models.Groups;
using EmailMarketing.Web.Areas.Member.Models;
using EmailMarketing.Web.Areas.Member.Models.ProfileModels;

using EmailMarketing.Web.Areas.Admin.Models.AdminUsers;
using EmailMarketing.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailMarketing.Web.Areas.Member.Models.Smtp;

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
            builder.RegisterType<SMTPModel>();
            builder.RegisterType<ChangeDefaultPasswordViewModel>();

            builder.RegisterType<MemberBaseModel>();
            base.Load(builder);
        }
    }
}  
