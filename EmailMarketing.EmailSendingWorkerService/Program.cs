using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmailMarketing.Common.Services;
using EmailMarketing.EmailSendingWorkerService.Entities;
using EmailMarketing.EmailSendingWorkerService.Services;
using EmailMarketing.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace EmailMarketing.EmailSendingWorkerService
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
                        .WriteTo.File("C://EmailMarketingTeamA//WorkerLogs//log.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();

            try
            {
                Log.Information("Email Sending Worker Service Staring up");
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "Email Sending Worker Service Start-up Failed");
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
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new FrameworkModule(_connectionString, 
                                _migrationAssemblyName));
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.Configure<WorkerSmtpSettings>(hostContext.Configuration.GetSection("SmtpSetting"));
                    services.AddSingleton<IMailerService, WorkerMailerService>();
                    services.AddSingleton<ICurrentUserService, WorkerCurrentUserService>();
                    services.AddSingleton<IDateTime, WorkerDateTimeService>();
                    services.AddHttpContextAccessor();
                });
    }
}
