using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using School.Application.Interfaces.Auth;
using School.Core.Model;
namespace School.Infrastructure;

public class JwtProvider(IOptions<JwtOptions> options): IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateJWTToken(User user)
    {
        var signingCredentials = new SigningCredentials( 
            new SymmetricSecurityKey( Encoding.UTF8.GetBytes(_options.SecretKey)), 
            SecurityAlgorithms.HmacSha256 );
        Claim[] claims = [ 
            new("userId", user.Id.ToString()),
            new("Admin", "true"),
        ];
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHourse));
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }
}