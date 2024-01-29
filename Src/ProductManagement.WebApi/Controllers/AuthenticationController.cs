using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Accounts.Commands.Login;
using ProductManagement.Application.Accounts.Commands.Register;
using ProductManagement.Application.Accounts.Commands.Token;

namespace ProductManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterCommandRequest registerCommandRequest)
        {
            //var result = await _authenticationService.RegisterUser(userForRegistration);
            RegisterCommandResponse result = await _mediator.Send(registerCommandRequest);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        [HttpPost("login")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] LoginCommangHandlerRequest loginCommangHandlerRequest)
        {
            LoginCommangHandlerResponse result = await _mediator.Send(loginCommangHandlerRequest);

            if (!result.IsValid)
                return Unauthorized();

            GenerateTokenCommandRequest generateTokenCommandRequest = new GenerateTokenCommandRequest
            {
                UserName = loginCommangHandlerRequest.UserName,
            };

            GenerateTokenCommandResponse tokenResult = await _mediator.Send(generateTokenCommandRequest);

            return Ok(tokenResult);
        }
    }
}
