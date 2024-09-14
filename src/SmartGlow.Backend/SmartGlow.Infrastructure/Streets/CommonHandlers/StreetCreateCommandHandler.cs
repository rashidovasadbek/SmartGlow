using AutoMapper;
using SmartGlow.Application.Streets.Commands;
using SmartGlow.Application.Streets.Models;
using SmartGlow.Application.Streets.Services;
using SmartGlow.Domain.Brokers;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Infrastructure.Streets.CommonHandlers;

/// <summary>
/// Handles the execution of the <see cref="StreetCreateCommand"/>.
/// Responsible for coordinating the creation of a new street.
/// </summary>
public class StreetCreateCommandHandler(
    IMapper mapper,
    IStreetService streetService,
    IRequestUserContextProvider requestUserContextProvider ) : ICommandHandler<StreetCreateCommand, StreetDto>
{
    public async Task<StreetDto> Handle(StreetCreateCommand request, CancellationToken cancellationToken)
    {
        request.Street.UserId = requestUserContextProvider.GetUserId();
        
        // Conversion to domain entity cancellationToken
        var street = mapper.Map<Street>(request.Street);
        
        var createdStreet = await streetService.CreateAsync(street, cancellationToken: cancellationToken);

        var streetDto = mapper.Map<StreetDto>(createdStreet);
        
        return streetDto;   
    }
}