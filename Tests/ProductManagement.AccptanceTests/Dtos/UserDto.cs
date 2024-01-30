using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ProductManagement.AccptanceTests.Dtos
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; init; }
        public string? Password { get; init; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
