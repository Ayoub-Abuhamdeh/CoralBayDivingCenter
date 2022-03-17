using CoralBayDivingCenter.Interfaces;
using CoralBayDivingCenter.Utils;
using CoralBayDivingCenter.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CoralBayDivingCenter.Helpers
{
    public static class DependencyInjectionContainer
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IStorageService, StorageService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IInternetService, InternetService>();

            return services;
        }

        public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
        {
            services.AddTransient<DiscoveryViewModel>();
            services.AddTransient<DivingLocationsViewModel>();
            services.AddTransient<TripsViewModel>();
            services.AddTransient<ProfileViewModel>();
            services.AddTransient<SignInViewModel>();
            services.AddTransient<TripDetailsViewModel>();
            services.AddTransient<DivingLocationDetailsViewModel>();

            return services;
        }
    }
}
