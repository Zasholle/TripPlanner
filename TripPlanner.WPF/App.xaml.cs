using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.Stores;
using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF
{
    public partial class App
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<UserStore>();
            services.AddSingleton<NavigationStore>();

            services.AddSingleton(CreateHomeNavigationService);

            services.AddTransient(s => new HomeViewModel(CreateLoginNavigationService(s)));
            services.AddTransient(s => new RegistryViewModel(CreateLoginNavigationService(s)));
            services.AddTransient(s => new LoginViewModel(CreateHomeNavigationService(s), CreateRegistryNavigationService(s)));
            services.AddSingleton<MainViewModel>();

            services.AddSingleton(s => new MainWindow
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
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

        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(
                CreateHomeNavigationService(serviceProvider),
                CreateRegistryNavigationService(serviceProvider),
                CreateLoginNavigationService(serviceProvider));
        }
    }
}
