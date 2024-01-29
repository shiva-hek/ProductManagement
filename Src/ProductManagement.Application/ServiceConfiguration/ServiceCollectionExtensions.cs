using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Accounts.Commands.Register;
using ProductManagement.Application.Mapping;

namespace ProductManagement.Application.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommandRequest).Assembly));
            return services;
        }
    }
}
