using MediatR;

namespace ProductManagement.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string CurrentUserId { get; set; }
    }
}
