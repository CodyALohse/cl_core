using Microsoft.Extensions.DependencyInjection;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework.Startup
{
    public static class ServiceCollectionExtensions
    {

        internal static IServiceCollection AddDataProviderSpecific<TContext>(this IServiceCollection services) where TContext: DbContext
        {
            // Update the IContextProvider and the IUnitOfWork to point to the 
            // specific data provider instances.
            services.AddScoped<IContextProvider, EntityContextProvider<TContext>>();
            services.AddScoped<IUnitOfWork, EntityUnitOfWork>();
            return services;
        }

        public static IServiceCollection AddDataProvider<TContext>(this IServiceCollection services) where TContext: DbContext
        {   
            services.AddDataProviderSpecific<TContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}