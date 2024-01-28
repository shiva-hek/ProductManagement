using ProductManagement.Domain.Aggregates.Products.ValueObjects;

namespace ProductManagement.UnitTests.Domain.Products
{
    public class ManufacturePhoneTests
    {
        [Fact]
        public void ManufacturePhone_WithValidFormat_ShouldBeCreated()
        {
            // Arrange
            string testPhone = "123456789";

            // Act & Assert
            Assert.NotNull(new ManufacturePhone(testPhone));
        }

        [Fact]
        public void ManufacturePhone_WithInvalidFormat_ShouldThrowException()
        {
            // Arrange
            string invalidPhone = "abcd123";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new ManufacturePhone(invalidPhone));
        }
    }
}
