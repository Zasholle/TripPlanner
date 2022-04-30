using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TripPlanner.WPF.ViewModels;
using TripPlanner.WPF.Views;

namespace TripPlanner.WPF.HostBuilders
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices((_,services) =>
            {
                services.AddSingleton<MainViewModel>();

                services.AddSingleton(s => new MainWindow
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });
            });

            return host;
        }
    }
}
