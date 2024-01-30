using ProductManagement.AccptanceTests.Context;
using ProductManagement.AccptanceTests.Drivers;
using ProductManagement.AccptanceTests.Dtos;
using ProductManagement.Application.Products.Queries.GetProducts;
using TechTalk.SpecFlow.Assist;

namespace ProductManagement.AccptanceTests.StepDefinitions
{
    [Binding]
    public class ProductStepDefinitions
    {
        private readonly ProductDriver _driver;
        protected readonly ProductContext ScenarioContext;

        public ProductStepDefinitions(ProductDriver driver, ProductContext scenarioContext)
        {
            _driver = driver;
            ScenarioContext = scenarioContext;
        }

        [Given(@"the database contains the following users:")]
        public async Task GivenTheDatabaseContainsTheFollowingUsers(Table table)
        {
            IEnumerable<UserDto>? Dtos = table.CreateSet<UserDto>();
            await _driver.SeedUsers(Dtos);
        }


        [Given(@"the following products are created:")]
        public async Task GivenTheFollowingProductsAreCreated(Table table)
        {
            IEnumerable<ProductDto>? Dtos = table.CreateSet<ProductDto>();
            await _driver.SeedProducts(Dtos);
        }


        [When(@"an authorized user with the Id ""([^""]*)"" attempts to create a products with the following attributes:")]
        public async Task WhenAnaAuthorizedUserWithTheIdAttemptsToCreateAProductsWithTheFollowingAttributes(string userId, Table table)
        {
            ProductDto product = table.CreateInstance<ProductDto>();
            product.CreatorId = userId;
            await _driver.CreateProduct(product);
        }


        [Then(@"the product should be created successfully")]
        public void ThenTheProductShouldBeCreatedSuccessfully()
        {
            Assert.NotNull(ScenarioContext.CreateProductResult);
            Assert.NotNull(ScenarioContext.CreateProductResult.Name);
        }


        [When(@"an authorized user with the Id ""([^""]*)"" attempts to create a duplicate product with the following attributes:")]
        public async Task WhenAUserWithTheIdAttemptsToCreateADuplicateProductWithTheFollowingAttributes(string userId, Table table)
        {
            ProductDto product = table.CreateInstance<ProductDto>();
            product.CreatorId = userId;
            await _driver.CreateProduct(product);
        }


        [Then(@"the instantiation should fail")]
        public void ThenTheInstantiationShouldFail()
        {
            ScenarioContext.ProductInstantiationFailed.Should().Be(true);
        }


        [When(@"an authorized user with the Id ""([^""]*)"" has created a product and attempts to update it as below")]
        public async Task WhenAUserWithTheIdHasCreatedAProductAndAttemptsToUpdateItAsBelow(string userId, Table table)
        {
            ProductDto product = table.CreateInstance<ProductDto>();
            product.CreatorId = userId;
            await _driver.UpdateProduct(product);
        }


        [Then(@"the product's name should be changed to ""([^""]*)""")]
        public void ThenTheProductsNameShouldBeChangedTo(string name)
        {
            ScenarioContext.UpdateProductResult.Name.Should().Be(name);
        }


        [Then(@"the nanufacturePhone should be changed to ""([^""]*)""")]
        public void ThenTheNanufacturePhoneShouldBeChangedTo(string phone)
        {
            ScenarioContext.UpdateProductResult.ManufacturePhone.Should().Be(phone);
        }


        [When(@"an authorized user with Id ""([^""]*)"" that is not the creator of a product attempts to update it as below")]
        public async Task WhenAUserWithIdThatIsNotTheCreatorOfAProductAttemptsToUpdateItAsBelow(string userId, Table table)
        {
            ProductDto product = table.CreateInstance<ProductDto>();
            product.CreatorId = userId;
            await _driver.UpdateProduct(product);
        }


        [Then(@"the system should reject the update attempt with an UnauthorizedAccessException")]
        public void ThenTheSystemShouldRejectTheUpdateAttemptWithAnUnauthorizedAccessException()
        {
            ScenarioContext.UnauthorizedException.Should().Be(true);
        }


        [When(@"an authorized user with the Id ""([^""]*)"" attempts to delete the follwing product that he has created before")]
        public async Task WhenAnAuthorizedUserWithTheIdAttemptsToDeleteTheFollwingProductThatHeHasCreatedBefore(string userId, Table table)
        {
            ProductDto product = table.CreateInstance<ProductDto>();
            product.CreatorId = userId;
            await _driver.DeleteProduct(product);
        }


        [Then(@"the product repository should not contain any product with Id ""([^""]*)""")]
        public async Task ThenTheProductRepositoryShouldNotContainAnyProductWithId(string productId)
        {
            await _driver.GetProduct(Guid.Parse(productId));
            ScenarioContext.GetProductResult.Should().Be(null);
        }


        [When(@"an authorized user with the Id ""([^""]*)"" attempts to delete the follwing product that is not creator of it")]
        public async Task WhenAnAuthorizedUserWithTheIdAttemptsToDeleteTheFollwingProductThatIsNotCreatorOfIt(string userId, Table table)
        {
            ProductDto product = table.CreateInstance<ProductDto>();
            product.CreatorId = userId;
            await _driver.DeleteProduct(product);
        }


        [When(@"a user attempts to filter products with the name of the creator containing ""([^""]*)""")]
        public async Task WhenAUserAttemptsToFilterProductsWithTheNameOfTheCreatorContaining(string creatorName)
        {
            await _driver.FilterProduct(creatorName);
        }


        [Then(@"the user should receive the following list of products:")]
        public void ThenTheUserShouldReceiveTheFollowingListOfProducts(Table table)
        {
            bool areEqual = true;
            var expectedProducts = table.CreateSet<FilterProductDto>();

            foreach (var expectedProduct in expectedProducts)
            {

                /* bool productFound = false*/
                ;
                foreach (var filteredProduct in ScenarioContext.FilterProductResult)
                {
                    if (AreProductsEqual(expectedProduct, filteredProduct))
                    {
                        //productFound = true;
                        //break;
                        areEqual = false;
                        break;
                    }
                }
            }

            areEqual.Should().Be(true);

        }


        private bool AreProductsEqual(FilterProductDto product1, GetProductsQueryResponse product2)
        {
            return product1.Name == product2.Name
                && product1.ProduceDate == product2.ProduceDate
                && product1.ManufacturePhone == product2.ManufacturePhone
                && product1.ManufactureEmail == product2.ManufactureEmail
                && product1.IsAvailable == product2.IsAvailable
                && product1.CreatorName == product2.CreatorName;
        }
    }
}

