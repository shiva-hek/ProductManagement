using ProductManagement.Domain.Aggregates.Products.ValueObjects;

namespace ProductManagement.UnitTests.Domain.Products
{
    public class NameTests
    {
        [Fact]
        public void Name_WithValidLength_ShouldBeCreated()
        {
            // Arrange
            string testName = "John Doe";

            // Act & Assert
            Assert.NotNull(new Name(testName));
        }

        [Fact]
        public void Name_WithEmptyString_ShouldThrowException()
        {
            // Arrange
            string emptyName = "";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new Name(emptyName));
        }

        [Fact]
        public void Name_WithNullString_ShouldThrowException()
        {
            // Arrange
            string nullName = null;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new Name(nullName));
        }

        [Fact]
        public void Name_WithTooLongString_ShouldThrowException()
        {
            // Arrange
            string tooLongName = "ThisIsANameThatIsTooLongAndExceedsTheMaximumAllowedLength";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new Name(tooLongName));
        }
    }
}
