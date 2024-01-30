using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProductManagement.AccptanceTests.Context;
using ProductManagement.AccptanceTests.Dtos;
using ProductManagement.AccptanceTests.Infrastructure;
using ProductManagement.Application.Products.Commands.CreateProduct;
using ProductManagement.Application.Products.Commands.DeleteProduct;
using ProductManagement.Application.Products.Commands.UpdateProduct;
using ProductManagement.Application.Products.Queries.GetProductById;
using ProductManagement.Application.Products.Queries.GetProducts;
using ProductManagement.Domain.Aggregates.Accounts;
using ProductManagement.Domain.Aggregates.Products;
using ProductManagement.Domain.Aggregates.Products.ValueObjects;
using ProductManagement.Domain.Services.Products;
using ProductManagement.Domain.SharedKernel;
using ProductManagement.Infrastructure.Persistence;

namespace ProductManagement.AccptanceTests.Drivers
{
    public class ProductDriver : IClassFixture<ProductWebApplicatioFactory>
    {
        private readonly ProductDbContext _dbContext;
        private readonly ProductWebApplicatioFactory _factory;
        protected readonly ProductContext ScenarioContext;
        private readonly HttpClient _httpClient;
        private readonly IMediator _mediator;

        public ProductDriver(ProductContext scenarioContext, ProductWebApplicatioFactory factory)
        {
            ScenarioContext = scenarioContext;
            _factory = factory;
            IServiceScope scope = _factory.Services.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            _httpClient = factory.CreateClient();
            _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        }

        public async Task SeedUsers(IEnumerable<UserDto> dtos)
        {
            List<User> doctors = GetUsers();
            await _dbContext.Users.AddRangeAsync(doctors);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SeedProducts(IEnumerable<ProductDto> dtos)
        {
            IEnumerable<Product> products = GetProducts(dtos);
            await _dbContext.Products.AddRangeAsync(products);
            await _dbContext.SaveChangesAsync();
        }

        private IEnumerable<Product> GetProducts(IEnumerable<ProductDto> dtos)
        {
            Mock<IProductUniquenessChecker> productUniquenessChecker = new Mock<IProductUniquenessChecker>();
            productUniquenessChecker.Setup(x => x.IsUnique(It.IsAny<ProduceDate>(), It.IsAny<ManufactureEmail>())).Returns(true);

            return dtos.Select(dto =>
                new Product(
                    dto.Id,
                    new Name(dto.Name),
                    new ProduceDate(dto.ProduceDate),
                    new ManufacturePhone(dto.ManufacturePhone),
                    new ManufactureEmail(dto.ManufactureEmail),
                    dto.IsAvailable,
                    dto.CreatorId,
                    productUniquenessChecker.Object
                )
            );
        }

        public async Task CreateProduct(ProductDto dto)
        {
            try
            {
                CreateProductCommandRequest request = new CreateProductCommandRequest();
                request.Name = dto.Name;
                request.ProduceDate = dto.ProduceDate;
                request.ManufacturePhone = dto.ManufacturePhone;
                request.ManufactureEmail = dto.ManufactureEmail;
                request.IsAvailable = dto.IsAvailable;
                request.CreatorId = dto.CreatorId;

                ScenarioContext.CreateProductResult = await _mediator.Send(request);
            }
            catch (InvalidOperationException)
            {
                ScenarioContext.ProductInstantiationFailed = true;
            }
            catch (BusinessRuleViolationException)
            {
                ScenarioContext.ProductInstantiationFailed = true;
            }

        }

        public async Task UpdateProduct(ProductDto dto)
        {
            try
            {
                UpdateProductCommandRequest request = new UpdateProductCommandRequest();
                request.ProductId = dto.Id;
                request.Name = dto.Name;
                request.ProduceDate = dto.ProduceDate;
                request.ManufacturePhone = dto.ManufacturePhone;
                request.ManufactureEmail = dto.ManufactureEmail;
                request.IsAvailable = dto.IsAvailable;
                request.CurrentUserId = dto.CreatorId;

                ScenarioContext.UpdateProductResult = await _mediator.Send(request);
            }
            catch (InvalidOperationException)
            {
                ScenarioContext.ProductUpdateFailed = true;
            }
            catch (BusinessRuleViolationException)
            {
                ScenarioContext.ProductUpdateFailed = true;
            }
            catch (UnauthorizedAccessException)
            {
                ScenarioContext.UnauthorizedException = true;
            }
        }

        public async Task DeleteProduct(ProductDto dto)
        {
            try
            {
                DeleteProductCommandRequest request = new DeleteProductCommandRequest();
                request.Id = dto.Id;
                request.CurrentUserId = dto.CreatorId;

                ScenarioContext.DeletedProductResult = await _mediator.Send(request);
            }
            catch (UnauthorizedAccessException)
            {
                ScenarioContext.UnauthorizedException = true;
            }
        }

        public async Task GetProduct(Guid id)
        {

            GetProductByIdQueryRequest request = new GetProductByIdQueryRequest();
            request.Id = id;

            ScenarioContext.GetProductResult = await _mediator.Send(request);
        }

        public async Task FilterProduct(string creatorName)
        {

            GetProductsQueryRequest request = new GetProductsQueryRequest();
            request.Name = creatorName;

            ScenarioContext.FilterProductResult = await _mediator.Send(request);
        }

        private List<User> GetUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    Id = "a149eff6-f08d-4b14-b8b3-16c25fd0e255",
                    FirstName = "Julie",
                    LastName = "Lerman",
                    UserName = "julie",
                    NormalizedUserName = "JULIE",
                    Email = "julie@gmail.com",
                    NormalizedEmail = "JULIE@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEMvCnGaivsPJiqV9fECZ7v0pI/WL0eyUModusPn1OlYlIYzGfeJwfN27zhNHeH9cIA==",
                    SecurityStamp = "BVH3QHWZQ4TCNAA2RRY4ASLWWYPWWNOP",
                    ConcurrencyStamp = "d5c57004-06a0-4f62-8981-7373d0b00b05",
                    PhoneNumber = "552233",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new User
                {
                    Id = "ab8e8e54-d7ca-4028-90d2-b7b3bba033ba",
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "john",
                    NormalizedUserName = "JOHN",
                    Email = "john@gmail.com",
                    NormalizedEmail = "JOHN@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAECYtjSXRLjEQTOCKlRzq4NcVs7KaHVAGELQYDcAce17mOF4UgK+WWdzWhlY2fK6xYw==",
                    SecurityStamp = "SMMCQFIPI5EXX3UPCWNRUOV6IWEDCYEA",
                    ConcurrencyStamp = "e8023bc0-d888-48e3-88c9-8d7695acd79b",
                    PhoneNumber = "559988",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            };

            return users;
        }

        private List<User> GetUsers1()
        {
            List<User> users = new List<User>();


            User user1 = new User
            {
                Id = "a149eff6-f08d-4b14-b8b3-16c25fd0e255",
                FirstName = "julie",
                LastName = "lerman",
                UserName = "julie",
                NormalizedUserName = "JULIE",
                Email = "julie@gmail.com",
                NormalizedEmail = "JULIE@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEMvCnGaivsPJiqV9fECZ7v0pI/WL0eyUModusPn1OlYlIYzGfeJwfN27zhNHeH9cIA==",
                SecurityStamp = "BVH3QHWZQ4TCNAA2RRY4ASLWWYPWWNOP",
                ConcurrencyStamp = "d5c57004-06a0-4f62-8981-7373d0b00b05",
                PhoneNumber = "552233",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            User user2 = new User
            {
                Id = "ab8e8e54-d7ca-4028-90d2-b7b3bba033ba",
                FirstName = "john",
                LastName = "doe",
                UserName = "john",
                NormalizedUserName = "JOHN",
                Email = "john@gmail.com",
                NormalizedEmail = "JOHN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAECYtjSXRLjEQTOCKlRzq4NcVs7KaHVAGELQYDcAce17mOF4UgK+WWdzWhlY2fK6xYw==",
                SecurityStamp = "SMMCQFIPI5EXX3UPCWNRUOV6IWEDCYEA",
                ConcurrencyStamp = "e8023bc0-d888-48e3-88c9-8d7695acd79b",
                PhoneNumber = "559988",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            users.Add(user1);
            users.Add(user2);

            return users;
        }
    }
}
