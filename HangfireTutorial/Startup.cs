using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(HangfireTutorial.Startup))]

namespace HangfireTutorial
{
    public partial class Startup
    {
        private IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("Data Source=MIRKO;Initial Catalog=HangfireTest;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });

            yield return new BackgroundJobServer();
        }
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(@"Data Source=MIRKO;Initial Catalog=HangfireTest;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //ConfigureAuth(app);

            app.UseHangfireDashboard("");
            app.UseHangfireServer();
            //BackgroundJob.Enqueue(() => Debug.WriteLine("Hello world from Hangfire!"));
            BackgroundJob.Enqueue(() => FireAndForget());

            BackgroundJob.Schedule(() => Console.WriteLine("schedule"), TimeSpan.FromMinutes(5));

            RecurringJob.AddOrUpdate(() => Console.WriteLine("recurrent"), Cron.Daily(18, 0));
        }

        public void FireAndForget()
        {
            Thread.Sleep(100000);
            Console.WriteLine("fire and forget");
        }

        private async Task CallEndpoint()
        {


            using (var _client = new HttpClient())
            {

            }
        }
    }
}
