using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection? services, IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            return services;
        }
    }
}