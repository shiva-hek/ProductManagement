using AutoMapper;
using MediatR;
using ProductManagement.Domain.Aggregates.Products;

namespace ProductManagement.Application.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, List<GetProductsQueryResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<GetProductsQueryResponse>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAllAsync(request.Name);

            List<GetProductsQueryResponse> result = _mapper.Map<List<GetProductsQueryResponse>>(products);

            return result;
        }
    }
}
