using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TripPlanner.EntityFramework;
using TripPlanner.WPF.Views;
using Microsoft.Extensions.Hosting;
using TripPlanner.WPF.HostBuilders;
using TripPlanner.WPF.Services;

namespace TripPlanner.WPF
{
    public partial class App
    {
        private readonly IHost _host;
        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[]? args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddDbContext()
                .AddServices()
                .AddStores()
                .AddViewModels()
                .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            var contextFactory = _host.Services.GetRequiredService<TripPlannerDbContextFactory>();
            
            using (var context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }

            var initialNavigationService = _host.Services.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}