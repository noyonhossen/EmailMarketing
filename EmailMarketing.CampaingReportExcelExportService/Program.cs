using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmailMarketing.CampaingReportExcelExportService.Entities;
using EmailMarketing.CampaingReportExcelExportService.Services;
using EmailMarketing.Common.Constants;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace EmailMarketing.CampaingReportExcelExportService
{
    public class Program
    {
        private static string _connectionString;
        private static string _migrationAssemblyName;
        public static void Main(string[] args)
        {
            _connectionString = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json", false)
                                    .Build()
                                    .GetConnectionString("DefaultConnection");

            _migrationAssemblyName = typeof(Worker).Assembly.FullName;

            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .WriteTo.File(ConstantsValue.WorkerExcelLogFileSaveUrl, rollingInterval: RollingInterval.Day)
                        .CreateLogger();

            try
            {
                Log.Information("Application Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog()
                .ConfigureContainer<ContainerBuilder>(builder => {
                    builder.RegisterModule(new FrameworkModule(_connectionString,
                        _migrationAssemblyName));
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.Configure<WorkerSmtpSettings>(hostContext.Configuration.GetSection("SmtpSetting"));
                    services.AddSingleton<IExportMailerService, WorkerMailerService>();
                    services.AddSingleton<ICurrentUserService, WorkerCurrentUserService>();
                    services.AddSingleton<IDateTime, WorkerDateTimeService>();
                    services.AddHttpContextAccessor();
                });
    }
}
