using ProductManagement.Application.Products.Commands.CreateProduct;
using ProductManagement.Application.Products.Commands.UpdateProduct;
using ProductManagement.Application.Products.Queries.GetProductById;
using ProductManagement.Application.Products.Queries.GetProducts;

namespace ProductManagement.AccptanceTests.Context
{
    public class ProductContext
    {
        public CreateProductCommandResponse CreateProductResult { get; set; } = null;
        public UpdateProductCommandResponse UpdateProductResult { get; set; } = null;
        public GetProductByIdQueryResponse GetProductResult { get; set; } = null;
        public List<GetProductsQueryResponse> FilterProductResult { get; set; } = null;

        public Guid DeletedProductResult { get; set; } = Guid.Empty;
        public bool ProductInstantiationFailed { get; set; } = false;
        public bool ProductUpdateFailed { get; set; } = false;
        public bool UnauthorizedException { get; set; } = false;
    }
}
