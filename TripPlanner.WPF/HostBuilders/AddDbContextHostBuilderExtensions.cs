using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TripPlanner.EntityFramework;

namespace TripPlanner.WPF.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("mssql");
                void ConfigureDbContext(DbContextOptionsBuilder o) => o.UseSqlServer(connectionString);

                services.AddDbContext<TripPlannerDbContext>((Action<DbContextOptionsBuilder>) ConfigureDbContext);
                services.AddSingleton(new TripPlannerDbContextFactory(ConfigureDbContext));
            });

            return host;
        }
    }
}
