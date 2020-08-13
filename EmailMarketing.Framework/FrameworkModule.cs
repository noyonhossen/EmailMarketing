using Autofac;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories;
using EmailMarketing.Framework.Repositories.Contacts;
using EmailMarketing.Framework.Repositories.Group;
using EmailMarketing.Framework.Repositories.Smtp;
using EmailMarketing.Framework.Services;
using EmailMarketing.Framework.Services.Contacts;
using EmailMarketing.Framework.Services.Groups;
using EmailMarketing.Framework.Services.Smtp;
using EmailMarketing.Framework.UnitOfWork;
using EmailMarketing.Framework.UnitOfWork.Smtp;
using EmailMarketing.Framework.UnitOfWork.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailMarketing.Framework.UnitOfWork.Group;

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

            builder.RegisterType<CampaignUnitOfWork>().As<ICampaignUnitOfWork>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<SMTPRepository>().As<ISMTPRepository>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<SMTPUnitOfWork>().As<ISMTPUnitOfWork>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<SMTPService>().As<ISmtpService>()
                   .InstancePerLifetimeScope();
                   
            builder.RegisterType<ContactExcelUnitOfWork>().As<IContactExcelUnitOfWork>()
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
            builder.RegisterType<ContactRepository>().As<IContactRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ContactUploadRepository>().As<IContactUploadRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FieldMapRepository>().As<IFieldMapRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ContactUploadFieldMapRepository>().As<IContactUploadFieldMapRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ContactValueMapRepository>().As<IContactValueMapRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GroupService>().As<IGroupService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ContactExcelService>().As<IContactExcelService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

