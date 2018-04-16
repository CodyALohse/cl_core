using Microsoft.Extensions.DependencyInjection;
using Core;

namespace Data.EntityFramework.Startup
{
    public static class ServiceCollectionExtensions
    {

        internal static IServiceCollection AddDataProviderSpecific(this IServiceCollection services)
        {
            // Update the IContextProvider and the IUnitOfWork to point to the 
            // specific data provider instances.
            services.AddScoped<IContextProvider, EntityContextProvider>();
            services.AddScoped<IUnitOfWork, EntityUnitOfWork>();
            return services;
        }

        public static IServiceCollection AddDataProvider(this IServiceCollection services)
        {   
            services.AddDataProviderSpecific();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}