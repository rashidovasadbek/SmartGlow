using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGlow.Application.Streets.Commands;
using SmartGlow.Application.Streets.Queries;
using IMediator = MediatR.IMediator;

namespace SmartGlow.Api.Controllers;

[Authorize]
[Controller]
[Route("api/[controller]")]
public class StreetsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] StreetGetQuery streetGetQuery, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(streetGetQuery, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }
    
    [HttpGet("{streetId:guid}")]
    public async Task<IActionResult> GetStreetById([FromRoute] Guid streetId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new StreetGetByIdQuery(){StreetId = streetId}, cancellationToken);
        return result is not null ? Ok(result) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStreet([FromBody] StreetCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrganization([FromBody] StreetUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{streetId:guid}")]
    public async ValueTask<IActionResult> DeleteStreetById([FromRoute] Guid streetId, CancellationToken cancellationToken = default)
    {
        var result =  await mediator.Send(new StreetDeleteByIdCommand(){ StreetId = streetId }, cancellationToken);
        return  result ? Ok() : BadRequest();
    }
}