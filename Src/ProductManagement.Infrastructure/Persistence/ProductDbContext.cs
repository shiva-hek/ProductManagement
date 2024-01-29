using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Aggregates.Accounts;
using ProductManagement.Domain.Aggregates.Products;

namespace ProductManagement.Infrastructure.Persistence
{
    public class ProductDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }


        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
