using ProductManagement.Domain.Aggregates.Products.ValueObjects;

namespace ProductManagement.UnitTests.Domain.Products
{
    public class ManufactureEmailTests
    {
        [Theory]
        [InlineData("validemail@example.com")]
        [InlineData("another.email@example.co.uk")]
        [InlineData("email-with-dashes@example-domain.com")]
        public void ManufactureEmail_WithValidEmailFormat_ShouldBeCreated(string email)
        {
            // Act & Assert
            Assert.NotNull(new ManufactureEmail(email));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ManufactureEmail_WithNullOrEmptyEmail_ShouldThrowException(string email)
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new ManufactureEmail(email));
        }

        [Theory]
        [InlineData("email@toolongexampleforcheckingcharahhgggghhhhhhhcterlength.com")]
        public void ManufactureEmail_WithInvalidLength_ShouldThrowException(string email)
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new ManufactureEmail(email));
        }

        [Theory]
        [InlineData("invalidemail")]
        [InlineData("invalidemail@")]
        [InlineData("invalidemail.com")]
        [InlineData("invalidemail@.com")]
        [InlineData("@invalidemail.com")]
        public void ManufactureEmail_WithInvalidFormat_ShouldThrowException(string email)
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new ManufactureEmail(email));
        }
    }


}
