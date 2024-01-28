using Moq;
using ProductManagement.Domain.Aggregates.Accounts;
using ProductManagement.Domain.Aggregates.Products;
using ProductManagement.Domain.Aggregates.Products.ValueObjects;
using ProductManagement.Domain.Services.Products;
using ProductManagement.Domain.SharedKernel;

namespace ProductManagement.UnitTests.Domain.Products
{
    public class ProductTest
    {
        private Mock<IProductUniquenessChecker> _productUniquenessCheckerMock;
        private Mock<IProductRepository> _productRepositoryMock;

        public ProductTest()
        {
            _productUniquenessCheckerMock = new Mock<IProductUniquenessChecker>();
            _productRepositoryMock = new Mock<IProductRepository>();

            _productUniquenessCheckerMock.Setup(
                    x => x.IsUnique(It.IsAny<ProduceDate>(), It.IsAny<ManufactureEmail>())).Returns(true);
        }

        [Fact]
        public void Product_ShouldBeAnEntity()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var name = new Name("Test Product");
            var produceDate = new ProduceDate(DateTime.Now);
            var manufacturePhone = new ManufacturePhone("123456");
            var manufactureEmail1 = new ManufactureEmail("test@example.com");
            var manufactureEmail2 = new ManufactureEmail("test@example.com");
            var isAvailable = true;
            var creatorId = "creator123";
            var creator = new User();

            // Act
            var product1 = new Product(productId, name, produceDate, manufacturePhone, manufactureEmail1, isAvailable, creatorId, creator, _productUniquenessCheckerMock.Object);
            var product2 = new Product(productId, name, produceDate, manufacturePhone, manufactureEmail2, isAvailable, creatorId, creator, _productUniquenessCheckerMock.Object);

            // Assert
            Assert.True(product1.Equals(product2));
            Assert.Equal(product1.GetHashCode(), product2.GetHashCode());
            Assert.IsAssignableFrom<Entity<Guid>>(product1);
        }

        [Fact]
        public void Constructor_ShouldCreateProduct_WhenValidParameters()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var name = new Name("Test Product");
            var produceDate = new ProduceDate(DateTime.Now);
            var manufacturePhone = new ManufacturePhone("123456");
            var manufactureEmail = new ManufactureEmail("test@example.com");
            var isAvailable = true;
            var creatorId = "creator123";
            var creator = new User();

            // Act
            var product = new Product(productId, name, produceDate, manufacturePhone, manufactureEmail, isAvailable, creatorId, creator, _productUniquenessCheckerMock.Object);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(productId, product.Id);
            Assert.Equal(name, product.Name);
            Assert.Equal(produceDate, product.ProduceDate);
            Assert.Equal(manufacturePhone, product.ManufacturePhone);
            Assert.Equal(manufactureEmail, product.ManufactureEmail);
            Assert.Equal(isAvailable, product.IsAvailable);
            Assert.Equal(creatorId, product.CreatorId);
            Assert.Equal(creator, product.Creator);
        }

        [Fact]
        public void Constructor_ThrowsException_WhenDuplicateManufactureEmailAndProductDateExist()
        {   // Arrange
            var productId = Guid.NewGuid();
            var name = new Name("Test Product");
            var produceDate = new ProduceDate(DateTime.Now);
            var manufacturePhone = new ManufacturePhone("123456");
            var manufactureEmail = new ManufactureEmail("test@example.com");
            var isAvailable = true;
            var creatorId = "creator123";
            var creator = new User();

            _productRepositoryMock.Setup(repo => repo.Get(produceDate, manufactureEmail))
                                   .ReturnsAsync(new Product(Guid.NewGuid(), name, produceDate, manufacturePhone, manufactureEmail, isAvailable, creatorId, creator, _productUniquenessCheckerMock.Object));

            // Act & Assert
            Assert.Throws<BusinessRuleViolationException>(() => new Product(productId, name, produceDate, manufacturePhone, manufactureEmail, isAvailable, creatorId, creator, new ProductUniquenessChecker(_productRepositoryMock.Object)));
        }

        [Fact]
        public void ChangeProductInfo_ThrowsException_WhenDuplicateManufactureEmailAndProductDateExist()
        {
            // Arrange
            DateTime prDate = DateTime.Now;
            var productId = Guid.NewGuid();
            var name = new Name("Test Product");
            var initialProduceDate = new ProduceDate(DateTime.Now.AddDays(-7)); // An initial produce date
            var newProduceDate = new ProduceDate(prDate); // A new produce date
            var manufacturePhone = new ManufacturePhone("123456");
            var initialManufactureEmail = new ManufactureEmail("initial@example.com"); // Initial manufacture email
            var newManufactureEmail = new ManufactureEmail("test@example.com"); // New manufacture email
            var isAvailable = true;
            var creatorId = "creator123";
            var creator = new User();

            var product = new Product(productId, name, initialProduceDate, manufacturePhone, initialManufactureEmail, isAvailable, creatorId, creator, _productUniquenessCheckerMock.Object);

            _productRepositoryMock.Setup(repo => repo.Get(newProduceDate, newManufactureEmail))
                                   .ReturnsAsync(new Product(Guid.NewGuid(), name, newProduceDate, manufacturePhone, newManufactureEmail, isAvailable, creatorId, creator, _productUniquenessCheckerMock.Object));

            // Act & Assert
            Assert.Throws<BusinessRuleViolationException>(()
                => product.SetProduceDateAndManufactureEmail(prDate, "test@example.com", new ProductUniquenessChecker(_productRepositoryMock.Object)));
        }


    }
}
