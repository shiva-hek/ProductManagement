using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Application.Accounts.Commands.Token
{
    public class GenerateTokenCommandRequest : IRequest<GenerateTokenCommandResponse>
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; init; }
    }
}
