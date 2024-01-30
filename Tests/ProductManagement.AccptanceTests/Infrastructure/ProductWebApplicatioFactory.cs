using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Domain.Services.Products;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.WebApi;

namespace ProductManagement.AccptanceTests.Infrastructure
{
    public class ProductWebApplicatioFactory : WebApplicationFactory<IApiMarker>
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInMemoryCollection(
                    new KeyValuePair<string, string>[]
                    {
                        new("UseInMemoryDatabase", "true"),
                        new("Token:ValidateLifetime", "false"),
                    });
            });

            builder.ConfigureTestServices((services) =>
            {
                ServiceProvider serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.Where(d => d.ServiceType == typeof(DbContextOptions<ProductDbContext>))
                    .ToList()
                    .ForEach(d => services.Remove(d));

                services.AddDbContext<ProductDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_dbName);
                    options.UseInternalServiceProvider(serviceProvider);
                });

                using IServiceScope scope = services.BuildServiceProvider().CreateScope();
                IServiceProvider scopedServices = scope.ServiceProvider;

                ProductDbContext db = scopedServices.GetRequiredService<ProductDbContext>();
                db.Database.EnsureCreated();

            });
        }
    }
}
