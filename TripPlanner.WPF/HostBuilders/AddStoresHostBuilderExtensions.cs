using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TripPlanner.WPF.Authenticators;
using TripPlanner.WPF.Stores;

namespace TripPlanner.WPF.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices((_,services) =>
            {
                services.AddSingleton<IAuthenticator, Authenticator>();
                services.AddSingleton<UserStore>();
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<HouseStore>();
            });

            return host;
        }
    }
}
