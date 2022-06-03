using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TripPlanner.WPF.Authenticators;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.Stores;
using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices((_, services) =>
            {
                services.AddSingleton(CreateLoginNavigationService);

                services.AddTransient(s => new HomeViewModel(CreateLoginNavigationService(s)));
                services.AddTransient(s => new RegistryViewModel(s.GetRequiredService<IAuthenticator>(), CreateLoginNavigationService(s)));
                services.AddTransient(s => new LoginViewModel(
                    s.GetRequiredService<IAuthenticator>(),
                    CreateHomeNavigationService(s), CreateRegistryNavigationService(s)));
                services.AddTransient<MainViewModel>();
            });

            return host;
        }

        private static INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<HomeViewModel>,
                () => CreateNavigationBarViewModel(serviceProvider));
        }

        private static INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<LoginViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<LoginViewModel>);
        }

        private static INavigationService CreateRegistryNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<RegistryViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<RegistryViewModel>);
        }

        private static NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(serviceProvider.GetRequiredService<UserStore>(),
                CreateHomeNavigationService(serviceProvider),
                CreateRegistryNavigationService(serviceProvider),
                CreateLoginNavigationService(serviceProvider));
        }
    }
}
