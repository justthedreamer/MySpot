using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySpot.Application.DTO;
using MySpot.Application.Security;
using MySpot.Core.Abstractions;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MySpot.Infrastructure.Auth;

internal class Authenticator(IOptions<AuthOptions> options, IClock clock) : IAuthenticator
{
    public JwtDto CreateToken(Guid userId, string role)
    {
        var issuer = options.Value.Issuer;
        var audience = options.Value.Audience;
        var expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)),
                SecurityAlgorithms.HmacSha256);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        
        
        var now = clock.Current();
        var expires = now.Add(expiry);
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new Claim(ClaimTypes.Role,role)
        };

        var jwt = new JwtSecurityToken(issuer, audience, claims,now ,expires, signingCredentials);
        var accessToken = jwtSecurityTokenHandler.WriteToken(jwt);

        return new JwtDto() { AccessToken = accessToken };
    }
}