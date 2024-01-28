using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Infrastructure.Persistence
{
    public static class ModelBuilderExtensions
    {
        // static readonly Guid productId = new Guid("0735B919-BD3A-48E2-A2BF-DD699E561940");
        // static readonly Guid userId = new Guid("504ECB9F-2A34-4FE9-9B89-24E2ACAC0620");

        //public static void SeedProducts(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>(b =>
        //    {
        //        b.HasData(new
        //        {
        //            Id = productId,
        //            IsAvailable = true,
        //            CreatorId = userId.ToString()
        //        });

        //        b.OwnsOne(e => e.Name).HasData(new
        //        {
        //            ProductId = productId, // Include ProductId property
        //            Value = "Iphone-12-10"
        //        });

        //        b.OwnsOne(e => e.ProduceDate).HasData(new
        //        {
        //            ProductId = productId, // Include ProductId property
        //            Value = DateTime.Now
        //        });

        //        b.OwnsOne(e => e.ManufacturePhone).HasData(new
        //        {
        //            ProductId = productId, // Include ProductId property
        //            Value = "332255"
        //        });

        //        b.OwnsOne(e => e.ManufactureEmail).HasData(new
        //        {
        //            ProductId = productId, // Include ProductId property
        //            Value = "iphone12@gmail.com"
        //        });

        //    });

        //    //SeedUsers(modelBuilder);
        //}


        //public static void SeedProducts(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>(b =>
        //    {
        //        b.HasData(new
        //        {
        //            Id = productId,
        //            IsAvailable = true,
        //            CreatorId = userId.ToString()
        //        });

        //        b.OwnsOne(e => e.Name).HasData(new
        //        {
        //            Value = "Iphone-12-10"

        //        });

        //        b.OwnsOne(e => e.ProduceDate).HasData(new
        //        {
        //            Value = DateTime.Now

        //        });

        //        b.OwnsOne(e => e.ManufacturePhone).HasData(new
        //        {
        //            Value = "332255"

        //        });

        //        b.OwnsOne(e => e.ManufactureEmail).HasData(new
        //        {
        //            Value = "iphone12@gmail.com"
        //        });

        //    });

        //    //SeedUsers(modelBuilder);
        //}

        //public static void SeedUsers(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>(b =>
        //    {
        //        b.HasData(new
        //        {
        //            Id = userId.ToString(),
        //            FirstName = "john",
        //            LastName = "doe",
        //            UserName = "john",
        //            Password = "12345678910",
        //            Email = "john@gmail.com",
        //            PhoneNumber = "332266",
        //        });

        //    });

        //}

        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                });
        }
    }
}
