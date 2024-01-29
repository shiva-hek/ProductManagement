using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProductManagement.Domain.Aggregates.Accounts;

namespace ProductManagement.Application.Accounts.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public RegisterCommandHandler(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, request.Roles);

            return _mapper.Map<RegisterCommandResponse>(result);
        }
    }
}
