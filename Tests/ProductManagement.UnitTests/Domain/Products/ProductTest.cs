namespace ProductManagement.UnitTests.Domain.Products
{
    public class ProductTest
    {
        [Fact]
        public void Product_ShouldBeAnEntity()
        {}

        [Fact]
        public void Constructor_ShouldCreateProduct_WhendValidParameters()
        {}

        [Fact]
        public void Constructor_ThrowsException_WhenProduceDateIsNotValid()
        { }

        [Fact]
        public void Constructor_ThrowsException_WhenManufactureEmailIsNotValid()
        { }

        [Fact]
        public void Constructor_ThrowsException_WhenManufacturePhoneIsNotValid()
        { }

        [Fact]
        public void Constructor_ThrowsException_WhenDuplicateManufactureEmailAndProductDateExist()
        { }

        [Fact]
        public void ChangeProductInfo_ThrowsException_WhenDuplicateManufactureEmailAndProductDateExist()
        { }
    }
}
