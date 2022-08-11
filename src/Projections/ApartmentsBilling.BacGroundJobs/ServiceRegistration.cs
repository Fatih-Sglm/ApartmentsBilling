
using ApartmentsBilling.BacGroundJobs.Features.Abstract;
using ApartmentsBilling.BacGroundJobs.Features.Abstract.AbstracService;
using ApartmentsBilling.BacGroundJobs.Features.Concrete.ConcreteService;
using ApartmentsBilling.BacGroundJobs.Features.Concrete.HangFireJobs;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApartmentsBilling.DataAccesLayer
{
    public static class ServiceRegistration
    {
        public static void HangfireSevices(this IServiceCollection services, IConfiguration configuration)
        {
            var hangFireDb = configuration.GetConnectionString("HangfireConnection");

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(hangFireDb, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer();

            services.AddScoped<IJobs, Jobs>();
            services.AddScoped<IMailSender, MailSender>();
        }
    }
}