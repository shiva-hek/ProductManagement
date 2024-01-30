using AutoMapper;
using MediatR;
using ProductManagement.Domain.Aggregates.Products;

namespace ProductManagement.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetAsync(request.Id);
            GetProductByIdQueryResponse mappedCustomer = _mapper.Map<GetProductByIdQueryResponse>(product);
            return mappedCustomer;
        }
    }
}
