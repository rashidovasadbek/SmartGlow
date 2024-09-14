using AutoMapper;
using SmartGlow.Application.Streets.Commands;
using SmartGlow.Application.Streets.Models;
using SmartGlow.Application.Streets.Services;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Infrastructure.Streets.CommonHandlers;

/// <summary>
/// Handles the StreetUpdateCommand by updating a product using the product service and mapping the result to a StreetDto.
/// </summary>
/// <param name="streetService"></param>
/// <param name="mapper"></param>
public class StreetUpdateCommandHandler(IMapper mapper, IStreetService streetService) : ICommandHandler<StreetUpdateCommand, StreetDto>
{
    public async Task<StreetDto> Handle(StreetUpdateCommand request, CancellationToken cancellationToken)
    {
        var street = mapper.Map<Street>(request.Street);
        
        var updatedStreet = await streetService.UpdateAsync(street, cancellationToken:cancellationToken);
        
        var streetDto = mapper.Map<StreetDto>(updatedStreet);

        return streetDto;
    }
}