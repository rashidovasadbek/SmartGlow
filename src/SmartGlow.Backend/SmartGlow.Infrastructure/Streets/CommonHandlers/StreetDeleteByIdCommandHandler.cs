using AutoMapper;
using SmartGlow.Application.Streets.Commands;
using SmartGlow.Application.Streets.Services;
using SmartGlow.Domain.Common.Commands;

namespace SmartGlow.Infrastructure.Streets.CommonHandlers;

public class StreetDeleteByIdCommandHandler(IMapper mapper, IStreetService streetService) : ICommandHandler<StreetDeleteByIdCommand , bool>
{
    public async Task<bool> Handle(StreetDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await streetService.DeleteByIdAsync(request.StreetId, cancellationToken: cancellationToken);
        
        return result is not null;
    }
}