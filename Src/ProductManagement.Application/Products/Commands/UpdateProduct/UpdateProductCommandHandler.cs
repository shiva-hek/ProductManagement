using AutoMapper;
using MediatR;
using ProductManagement.Domain.Aggregates.Products;
using ProductManagement.Domain.Services.Products;

namespace ProductManagement.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IProductUniquenessChecker _productUniquenessChecker;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IProductUniquenessChecker productUniquenessChecker)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productUniquenessChecker = productUniquenessChecker;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken = default)
        {
            Product product = await _productRepository.GetAsync(request.ProductId);

            if (product == null)
                return default;

            if (product.CreatorId != request.CurrentUserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to edit this product.");
            }

            product.SetProduceDateAndManufactureEmail(request.ProduceDate, request.ManufactureEmail, _productUniquenessChecker);
            product.SetProductInfo(request.Name, request.ManufacturePhone, request.IsAvailable);

            await _productRepository.UpdateAsync(product);
            UpdateProductCommandResponse mappedProduct = _mapper.Map<UpdateProductCommandResponse>(product);
            return mappedProduct;
        }
    }
}
