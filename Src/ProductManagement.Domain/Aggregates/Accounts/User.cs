using Microsoft.AspNetCore.Identity;

namespace ProductManagement.Domain.Aggregates.Accounts
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; }= null!;
    }
}
