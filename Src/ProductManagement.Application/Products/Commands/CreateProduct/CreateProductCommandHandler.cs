using AutoMapper;
using MediatR;
using ProductManagement.Domain.Aggregates.Products;
using ProductManagement.Domain.Aggregates.Products.Factories;
using System.Diagnostics.CodeAnalysis;

namespace ProductManagement.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductFactory _productFactory;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(
            IProductRepository productRepository,
            ProductFactory productFactory,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productFactory = productFactory;
            _mapper = mapper;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken = default)
        {
            Product product = _productFactory.Create(
                request.Name,
                request.ProduceDate,
                request.ManufacturePhone,
                request.ManufactureEmail,
                request.IsAvailable,
                request.CreatorId);


            await _productRepository.InsertAsync(product);
            CreateProductCommandResponse mappedProduct = _mapper.Map<CreateProductCommandResponse>(product);
            return mappedProduct;
        }
    }
}
