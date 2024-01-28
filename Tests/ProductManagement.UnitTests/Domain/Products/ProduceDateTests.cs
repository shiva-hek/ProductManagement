using ProductManagement.Domain.Aggregates.Products.ValueObjects;

namespace ProductManagement.UnitTests.Domain.Products
{
    public class ProduceDateTests
    {
        [Fact]
        public void ProduceDate_WithValidDate_ShouldSetProduceDate()
        {
            // Arrange
            DateTime validDate = DateTime.Now.AddDays(-1); // A date in the past

            // Act
            var produceDate = new ProduceDate(validDate);

            // Assert
            Assert.Equal(validDate, produceDate.Value);
        }

        [Fact]
        public void ProduceDate_WithInvalidDate_ShouldThrowException()
        {
            // Arrange
            DateTime invalidDate = DateTime.Now.AddDays(1); // A date in the future

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new ProduceDate(invalidDate));
        }

        [Fact]
        public void ProduceDate_Equals_ReturnsTrueForEqualObjects()
        {
            // Arrange
            var date1 = new ProduceDate(new DateTime(2023, 1, 1));
            var date2 = new ProduceDate(new DateTime(2023, 1, 1));

            // Act & Assert
            Assert.Equal(date1, date2);
        }

        [Fact]
        public void ProduceDate_Equals_ReturnsFalseForDifferentObjects()
        {
            // Arrange
            var date1 = new ProduceDate(new DateTime(2023, 1, 1));
            var date2 = new ProduceDate(new DateTime(2023, 1, 2));

            // Act & Assert
            Assert.NotEqual(date1, date2);
        }
    }

}
