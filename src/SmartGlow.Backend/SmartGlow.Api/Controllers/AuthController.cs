using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGlow.Application.Common.Identity.Commands;
using SmartGlow.Application.Common.Identity.Models;
using SmartGlow.Application.Common.Identity.Queries;
using SmartGlow.Application.Common.Identity.Services;
using SmartGlow.Application.Users.Models;


namespace SmartGlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMapper mapper, IMediator mediator, IAuthService authService): ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDetails signUpDetails, CancellationToken cancellationToken)
    {
        var result = await authService.SignUpAsync(signUpDetails, cancellationToken);
        return result ? Ok(result) : BadRequest();
    }
    
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInDetails signInDetails, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new SignInCommand {SignInDetails = signInDetails}, cancellationToken);
        return Ok(mapper.Map<IdentityTokenDto>(result));
    }
    
    [HttpPost("sign-out")]
    public async ValueTask<IActionResult> SignOutAsync([FromBody]string refreshToken, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new SignOutCommand {RefreshToken = refreshToken}, cancellationToken);
        return result ? Ok(result) : BadRequest();
    }
    
    [HttpPut("refresh-token")]
    public async ValueTask<IActionResult> RefreshToken([FromQuery] string refreshTokenValue, CancellationToken cancellationToken)
    {
        var result = await authService.RefreshTokenAsync(refreshTokenValue, cancellationToken);
        return Ok(mapper.Map<AccessTokenDto>(result));
    }
    
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCurrentUserQuery(), cancellationToken);
        return Ok(mapper.Map<UserDto>(result));
    }
}