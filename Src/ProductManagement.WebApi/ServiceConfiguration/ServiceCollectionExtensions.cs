using Microsoft.AspNetCore.Identity;
using ProductManagement.Domain.Aggregates.Accounts;
using ProductManagement.Infrastructure.Persistence;

namespace ProductManagement.WebApi.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ProductDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
