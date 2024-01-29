using MediatR;
using ProductManagement.Domain.Aggregates.Products;

namespace ProductManagement.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, Guid>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken = default)
        {
            Product product = await _productRepository.GetAsync(request.Id);

            if (product == null)
                return default;

            if (product.CreatorId != request.CurrentUserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to delete this product.");
            }

            await _productRepository.RemoveAsync(request.Id);
            return request.Id;
        }
    }
}
