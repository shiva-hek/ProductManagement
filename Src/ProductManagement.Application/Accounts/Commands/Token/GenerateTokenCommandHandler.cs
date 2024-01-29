using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Domain.Aggregates.Accounts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagement.Application.Accounts.Commands.Token
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommandRequest, GenerateTokenCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User? _user;

        public GenerateTokenCommandHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<GenerateTokenCommandResponse> Handle(GenerateTokenCommandRequest request, CancellationToken cancellationToken)
        {
            GenerateTokenCommandResponse result = new GenerateTokenCommandResponse();
            var signingCredentials = GetSigningCredentials();

            var claims = await GetClaims(request.UserName);

            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            result.AccessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return result;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(string userName)
        {
            _user = await _userManager.FindByNameAsync(userName);

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user.UserName), new Claim(ClaimTypes.NameIdentifier, _user.Id) };
            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
                (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
                );

            return tokenOptions;
        }
    }
}
