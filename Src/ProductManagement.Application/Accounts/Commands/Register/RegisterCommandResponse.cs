using Microsoft.AspNetCore.Identity;

namespace ProductManagement.Application.Accounts.Commands.Register
{
    public class RegisterCommandResponse
    {
        public bool Succeeded { get; set; }
        public IEnumerable<IdentityErrorDto> Errors { get; set; }
    }
}
