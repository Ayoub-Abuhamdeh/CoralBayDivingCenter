using CoralBayDivingCenter.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoralBayDivingCenter
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IServiceCollection ServiceCollection = new ServiceCollection();

        public static IServiceProvider Init()
        {
            _ = ServiceCollection
           .ConfigureServices()
           .ConfigureViewModels();

            ServiceProvider = ServiceCollection.BuildServiceProvider();
            return ServiceProvider;
        }

        // To get any service without injecting it you can call it as followed:
        // Startup.ServiceProvider.GetService<MainViewModel>();
    }
}
