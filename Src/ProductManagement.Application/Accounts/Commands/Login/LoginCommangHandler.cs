using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ProductManagement.Domain.Aggregates.Accounts;

namespace ProductManagement.Application.Accounts.Commands.Login
{
    public class LoginCommangHandler : IRequestHandler<LoginCommangHandlerRequest, LoginCommangHandlerResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User? _user;

        public LoginCommangHandler(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginCommangHandlerResponse> Handle(LoginCommangHandlerRequest request, CancellationToken cancellationToken)
        {
            LoginCommangHandlerResponse result = new LoginCommangHandlerResponse();

            _user = await _userManager.FindByNameAsync(request.UserName);

            result.IsValid = (_user != null && await _userManager.CheckPasswordAsync(_user, request.Password));

            return result;
        }
    }
}
