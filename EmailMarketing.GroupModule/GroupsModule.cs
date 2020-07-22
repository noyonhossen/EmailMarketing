using Autofac;
using EmailMarketing.GroupModule.Context;
using EmailMarketing.GroupModule.Repositories;
using EmailMarketing.GroupModule.Services;
using EmailMarketing.GroupModule.UnitOfWork;
using System;

namespace EmailMarketing.GroupModule
{
    public class GroupsModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public GroupsModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GroupContext>()
                   .WithParameter("connectionString", _connectionString)
                   .WithParameter("migrationAssemblyName", _migrationAssemblyName)
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
