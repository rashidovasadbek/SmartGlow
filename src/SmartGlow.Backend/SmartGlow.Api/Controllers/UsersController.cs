using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartGlow.Application.Common.Identity.Queries;
using SmartGlow.Application.Users.Commands;
using SmartGlow.Application.Users.Queries;

namespace SmartGlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator): ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] UserGetQuery clientGetQuery, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(clientGetQuery, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{userId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UserGetByIdQuery(){UserId = userId}, cancellationToken);
        return result is not null ? Ok(result) : NoContent();
    }

    [HttpGet("by-username/{username}")]
    public async ValueTask<IActionResult> CheckClientByUserName([FromRoute] string username, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CheckUserByUserNameQuery() { UserName = username }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }
    
    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] UserUpdateCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{userid:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UserDeleteByIdCommand() {UserId = userId}, cancellationToken);
        return result ? Ok() : BadRequest();
    }
}