using System.Security.Authentication;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartGlow.Application.Common.Identity.Models;
using SmartGlow.Application.Common.Identity.Services;
using SmartGlow.Application.Users.Services;
using SmartGlow.Domain.Brokers;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Infrastructure.Common.Identity.Services;

public class AuthService(
    IMapper mapper,
    IUserService userService,
    IAccountService accountService,
    IIdentitySecurityTokenService identitySecurityTokenService,
    IIdentitySecurityTokenGeneratorService identitySecurityTokenGeneratorService,
    IPasswordHasherService passwordHasherService,
    IPasswordGeneratorService passwordGeneratorService,
    IRequestUserContextProvider requestUserContextProvider)
    : IAuthService
{
   
    private async Task<(AccessToken AccessToken, RefreshToken RefreshToken)> CreateTokens(User user, bool rememberMe, CancellationToken cancellationToken = default)
    {
        var accessToken = identitySecurityTokenGeneratorService.GenerateAccessToken(user);
        var refreshToken = identitySecurityTokenGeneratorService.GenerateRefreshToken(user, rememberMe);

        return (await identitySecurityTokenService.CreateAccessTokenAsync(accessToken, new CommandOptions(), cancellationToken),
            await identitySecurityTokenService.CreateRefreshTokenAsync(refreshToken, new CommandOptions(), cancellationToken));
    }

    public async ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        //Check that the user is in the database at the entered email address
        var foundUserId = await userService.Get(queryOptions: new QueryOptions(){ TrackingMode = QueryTrackingMode.AsNoTracking })
            .FirstOrDefaultAsync(client => client.UserName == signUpDetails.UserName, cancellationToken);
       
        if (foundUserId is not null)
            throw new InvalidOperationException("User with this email address already exists.");

        //Map the entered user object
        var client = mapper.Map<User>(signUpDetails);

        //Generate complex password and hash it
        client.PasswordHash = passwordHasherService.HashPassword(passwordGeneratorService.GeneratePassword());

        var createdUser = await accountService.CreateUserAsync(client, cancellationToken);
        
        return createdUser is not null;
    }

    public async ValueTask<(AccessToken accessToken, RefreshToken refreshToken)> SignInUsernameAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default)
    {
        // Query user by username
        var foundUser = 
            await userService.Get(user => user.UserName == signInDetails.Username,
                    queryOptions: new QueryOptions { TrackingMode = QueryTrackingMode.AsNoTracking })
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        if (foundUser is null || !passwordHasherService.ValidatePassword(signInDetails.Password, foundUser.PasswordHash))
            throw new AuthenticationException("Sign in details are invalid, contact support.");
       
        return await CreateTokens(foundUser, signInDetails.RememberMe, cancellationToken);
    }

    public async ValueTask SignOutAsync(string accessTokenValue, string refreshTokenValue,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(accessTokenValue) || string.IsNullOrWhiteSpace(refreshTokenValue))
            throw new ArgumentException("Invalid identity security token value", nameof(accessTokenValue));

        await identitySecurityTokenService.RemoveRefreshTokenAsync(refreshTokenValue, cancellationToken);

        var accessToken = identitySecurityTokenGeneratorService.GetAccessToken(accessTokenValue);
        if (accessToken.HasValue) 
            await identitySecurityTokenService.RemoveAccessTokenAsync(accessToken.Value.AccessToken.Id, cancellationToken);
    }

    public async ValueTask<AccessToken> RefreshTokenAsync(string refreshTokenValue, CancellationToken cancellationToken = default)
    {
        var accessTokenValue = requestUserContextProvider.GetAccessToken();

        if (string.IsNullOrWhiteSpace(refreshTokenValue))
            throw new ArgumentException("Invalid identity security token value", nameof(refreshTokenValue));

        if (string.IsNullOrWhiteSpace(accessTokenValue))
            throw new InvalidOperationException("Invalid identity security token value");

        // Check refresh token and access token
        var refreshToken = await identitySecurityTokenService.GetRefreshTokenByValueAsync(refreshTokenValue, cancellationToken);
        if (refreshToken is null)
            throw new AuthenticationException("Please login again.");

        var accessToken = identitySecurityTokenGeneratorService.GetAccessToken(accessTokenValue);
        if (!accessToken.HasValue)
        {
            // Remove refresh token if access token is not valid
            await identitySecurityTokenService.RemoveRefreshTokenAsync(refreshTokenValue, cancellationToken);
            throw new InvalidOperationException("Invalid identity security token value");
        }

        var foundAccessToken = await identitySecurityTokenService.GetAccessTokenByIdAsync(accessToken.Value.AccessToken.Id, cancellationToken);

        // Remove refresh token and access token if user id is not same
        if (refreshToken.UserId != accessToken.Value.AccessToken.UserId)
        {
            await identitySecurityTokenService.RemoveRefreshTokenAsync(refreshTokenValue, cancellationToken);
            if (foundAccessToken is not null)
                await identitySecurityTokenService.RevokeAccessTokenAsync(accessToken.Value.AccessToken.Id, cancellationToken);

            throw new AuthenticationException("Please login again.");
        }

        var foundUser = await userService.Get(
                user => user.Id == accessToken.Value.AccessToken.UserId,
                new QueryOptions
                {
                    TrackingMode = QueryTrackingMode.AsNoTracking
                }
            )
            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new InvalidOperationException();

        // If access token exists, not revoked and still valid return it, otherwise remove
        if (foundAccessToken is not null && !foundAccessToken.IsRevoked)
        {
            if (!foundAccessToken.IsRevoked)
                return foundAccessToken;
            await identitySecurityTokenService.RemoveAccessTokenAsync(accessToken.Value.AccessToken.Id, cancellationToken);
        }

        // Generate access token
        var newAccessToken = identitySecurityTokenGeneratorService.GenerateAccessToken(foundUser);

        return await identitySecurityTokenService.CreateAccessTokenAsync(newAccessToken, new CommandOptions(), cancellationToken);
    
    }
}