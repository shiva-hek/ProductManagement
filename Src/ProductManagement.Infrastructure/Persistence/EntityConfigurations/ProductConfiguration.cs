using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Domain.Aggregates.Products;

namespace ProductManagement.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                        .HasColumnName("Id");

            builder.OwnsOne(e => e.Name, name =>
            {
                name.Property(p => p.Value)
                    .HasColumnType("nvarchar(50)")
                    .IsRequired()
                    .HasColumnName("Name");

            }).Navigation(product => product.Name).IsRequired();


            builder.OwnsOne(e => e.ProduceDate, produceDate =>
            {
                produceDate.Property(p => p.Value)
                    .HasColumnType("datetime")
                    .IsRequired()
                    .HasColumnName("ProduceDate");

            }).Navigation(product => product.ProduceDate).IsRequired();


            builder.OwnsOne(e => e.ManufacturePhone, manufacturePhone =>
            {
                manufacturePhone.Property(p => p.Value)
                    .HasColumnType("nvarchar(20)")
                    .IsRequired()
                    .HasColumnName("ManufacturePhone");

            });

            builder.OwnsOne(e => e.ManufactureEmail, manufactureEmail =>
            {
                manufactureEmail.Property(p => p.Value)
                    .HasColumnType("nvarchar(50)")
                    .IsRequired()
                    .HasColumnName("ManufactureEmail");

            }).Navigation(product => product.ManufactureEmail).IsRequired();

            builder.Property(e => e.IsAvailable)
                .HasColumnType("bit")
                .IsRequired()
                .HasColumnName("IsAvailable");


            builder.HasOne(p => p.Creator)
                .WithMany()
                .HasForeignKey(p => p.CreatorId);
        }
    }
}
