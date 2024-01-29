using MediatR;

namespace ProductManagement.Application.Products.Queries.GetProducts
{
    public class GetProductsQueryRequest:IRequest<List<GetProductsQueryResponse>>
    {
        public string Name { get; set; } = null;
    }
}
