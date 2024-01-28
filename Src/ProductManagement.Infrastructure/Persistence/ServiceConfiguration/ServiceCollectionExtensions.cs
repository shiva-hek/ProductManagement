using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Domain.Aggregates.Products;
using ProductManagement.Infrastructure.Persistence.Repositories;

namespace ProductManagement.Infrastructure.Persistence.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
