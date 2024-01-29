using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Application.Accounts.Commands.Login
{
    public class LoginCommangHandlerRequest : IRequest<LoginCommangHandlerResponse>
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password name is required")]
        public string? Password { get; init; }
    }
}
