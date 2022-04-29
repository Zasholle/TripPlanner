using System;
using System.Windows;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TripPlanner.Domain.Models;
using TripPlanner.Domain.Services;
using TripPlanner.Domain.Services.Authentication;
using TripPlanner.EntityFramework;
using TripPlanner.EntityFramework.Services;
using TripPlanner.WPF.Authenticators;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.Stores;
using TripPlanner.WPF.ViewModels;
using TripPlanner.WPF.Views;

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
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<IUserService, UserDataService>();
            services.AddDbContext<TripPlannerDbContext>();
            services.AddSingleton(new TripPlannerDbContextFactory());
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddSingleton(CreateHomeNavigationService);

            services.AddTransient(s => new HomeViewModel(CreateLoginNavigationService(s)));
            services.AddTransient(s => new RegistryViewModel(s.GetRequiredService<IAuthenticator>(), CreateLoginNavigationService(s)));
            services.AddTransient(s => new LoginViewModel(
                s.GetRequiredService<IAuthenticator>(),
                CreateHomeNavigationService(s), CreateRegistryNavigationService(s)));
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
            return new NavigationBarViewModel(serviceProvider.GetRequiredService<UserStore>(),
                CreateHomeNavigationService(serviceProvider),
                CreateRegistryNavigationService(serviceProvider),
                CreateLoginNavigationService(serviceProvider));
        }
    }
}
