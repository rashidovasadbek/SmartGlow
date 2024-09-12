using AutoMapper;
using SmartGlow.Application.Users.Commands;
using SmartGlow.Application.Users.Models;
using SmartGlow.Application.Users.Services;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Infrastructure.Users.CommandHandlers;

public class UserUpdateCommandHandler(IUserService userService, IMapper mapper) :ICommandHandler<UserUpdateCommand, UserDto>
{
    public async Task<UserDto> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        var client = mapper.Map<User>(request.User); 
        var updatedClient = await userService.UpdateAsync(client, cancellationToken: cancellationToken);
        
        return mapper.Map<UserDto>(updatedClient);
    }
}