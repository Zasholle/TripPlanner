﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TripPlanner.Domain.Models;
using TripPlanner.Domain.Services.Authentication;
using TripPlanner.Domain.Services.Data;
using TripPlanner.EntityFramework.Services;
using TripPlanner.WPF.Services;
using HouseDataService = TripPlanner.EntityFramework.Services.HouseDataService;

namespace TripPlanner.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((_,services) =>
            {
                services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<IDataService<User>, UserDataService>();
                services.AddSingleton<IUserService, UserDataService>();
                services.AddSingleton<IDataService<House>, HouseDataService>();
                services.AddSingleton<IHouseService, HouseDataService>();
                services.AddSingleton<IHouseDataService, HouseService>();
            });

            return host;
        }
    }
}
