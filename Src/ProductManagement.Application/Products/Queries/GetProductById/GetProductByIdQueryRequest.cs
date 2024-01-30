using MediatR;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace ProductManagement.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
