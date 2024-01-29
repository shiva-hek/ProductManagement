using MediatR;

namespace ProductManagement.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandRequest:IRequest<UpdateProductCommandResponse>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
        public string CurrentUserId { get; set; }
    }
}
