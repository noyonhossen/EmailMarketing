using Autofac;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories;
using EmailMarketing.Framework.Services;
using EmailMarketing.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework
{
    public class FrameworkModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public FrameworkModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FrameworkContext>()
                   .WithParameter("connectionString", _connectionString)
                   .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                   .InstancePerLifetimeScope();

            builder.RegisterType<ExpenseUnitOfWork>().As<IExpenseUnitOfWork>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ExpenseRepository>().As<IExpenseRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExpenseService>().As<IExpenseService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<GroupUnitOfWork>().As<IGroupUnitOfWork>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<GroupRepository>().As<IGroupRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GroupService>().As<IGroupService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

