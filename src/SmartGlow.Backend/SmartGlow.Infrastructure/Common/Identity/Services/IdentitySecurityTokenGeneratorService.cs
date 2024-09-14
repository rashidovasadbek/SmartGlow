using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartGlow.Application.Common.Identity.Services;
using SmartGlow.Domain.Constants;
using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Extensions;
using SmartGlow.Infrastructure.Common.Settings;

namespace SmartGlow.Infrastructure.Common.Identity.Services;

public class IdentitySecurityTokenGeneratorService(IOptions<JwtSettings> jwtSettings): IIdentitySecurityTokenGeneratorService
{
    
    /// <summary>
    /// Initializes a new instance of the AccessTokenGeneratorService class.
    /// </summary>
    /// <param name="jwtSettings">Options for configuring JWT settings injected via dependency injection.</param>
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public AccessToken GenerateAccessToken(User user)
    {
        var accessToken = new AccessToken()
        {
            Id = Guid.NewGuid()
        };

        var jwtToken = GetToken(user, accessToken);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        accessToken.Token = token;

        return accessToken;
    }

    public RefreshToken GenerateRefreshToken(User user, bool extendedExpiryTime = false)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return new RefreshToken()
        {
            Id = Guid.NewGuid(),
            Token = Convert.ToBase64String(randomNumber),
            UserId = user.Id,
            ExpiryTime = DateTime.UtcNow.AddMinutes(
                extendedExpiryTime
                    ? _jwtSettings.RefreshTokenExtendedExpirationTimeInMinutes
                    : _jwtSettings.RefreshTokenExpirationTimeInMinutes
            )
        };
    }

    public (AccessToken AccessToken, bool IsExpired)? GetAccessToken(string tokenValue)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var getAccessToken = () =>
        {
            var tokenWithPrefix = tokenValue.Replace("Bearer ", string.Empty);

            var tokenValidationParameters = _jwtSettings.MapToTokenValidationParameters();
            tokenValidationParameters.ValidateLifetime = false;

            var principal = tokenHandler.ValidateToken(tokenWithPrefix, tokenValidationParameters, out var validatedToken);
            
            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                ))
                throw new SecurityTokenException("Invalid token");
            
            var isExpired = jwtSecurityToken.ValidTo.ToUniversalTime() < DateTime.UtcNow;

            return (new AccessToken
            {
                Id = Guid.Parse(principal.FindFirst(JwtRegisteredClaimNames.Jti)!.Value),
                UserId = Guid.Parse(principal.FindFirst(ClaimConstants.UserId)!.Value),
                Token = tokenValue,
                ExpiryTime = jwtSecurityToken.ValidTo.ToUniversalTime()
            }, isExpired);
        };

        return getAccessToken.GetValue().Data;
    }

    public Guid GetAccessTokenId(string accessToken)
    {
        // Extract the token value from the authorization header.
        var tokenValue = accessToken.Split(' ')[1];

        // Create a JwtSecurityTokenHandler to read and parse the JWT token.
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(tokenValue);

        // Retrieve the unique identifier (ID) claim from the token.
        var tokenId = token.Claims.FirstOrDefault(c => c.Type == ClaimConstants.AccessTokenId)?.Value;

        // Validate and parse the retrieved ID, throwing an exception if it is invalid or missing.
        if (string.IsNullOrEmpty(tokenId))
            throw new ArgumentException("Invalid AccessToken");

        return Guid.Parse(tokenId);
    }

    /// <summary>
    /// Generates a JWT security token for the specified client and access token.
    /// </summary>
    /// <param name="user">The client for which the token is generated.</param>
    /// <param name="accessToken">The access token associated with the client.</param>
    /// <returns>A JWT security token.</returns>
    public JwtSecurityToken GetToken(User user, AccessToken accessToken)
    {
        // Generate claims for the client
        var claims = GetClaims(user, accessToken);

        // Update access token properties
        accessToken.UserId = user.Id;
        accessToken.ExpiryTime = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes);

        // Create a security key using the JWT secret key
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        // Create signing credentials using the security key
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Create and return a JWT security token
        return new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: accessToken.ExpiryTime.UtcDateTime,
            signingCredentials: credentials
        );
    }

    public List<Claim> GetClaims(User user, AccessToken accessToken)
    {
        return new List<Claim>()
        {
            // Claim representing the email address of the client
            new(ClaimTypes.Name, user.UserName),
            
            // Claim representing the role of the client
            new(ClaimTypes.Role, user.Role.ToString()),
        
            // Claim representing the user ID
            new(ClaimConstants.UserId, user.Id.ToString()),
        
            // Claim representing the ID of the access token
            new(JwtRegisteredClaimNames.Jti, accessToken.Id.ToString())
        };
    }
}