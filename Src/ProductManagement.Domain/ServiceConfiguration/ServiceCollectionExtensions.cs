using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Domain.Aggregates.Products.Factories;
using ProductManagement.Domain.Services.Products;

namespace ProductManagement.Domain.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ProductFactory));
            services.AddScoped<IProductUniquenessChecker, ProductUniquenessChecker>();

            return services;
        }
    }
}
