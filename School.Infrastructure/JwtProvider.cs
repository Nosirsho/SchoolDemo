using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using School.Application.Interfaces.Auth;
using School.Application.Services;
using School.Core.Enums;
using School.Core.Model;
namespace School.Infrastructure;

public class JwtProvider(IOptions<JwtOptions> options): IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateJWTToken(User user, HashSet<string> permissions)
    {
        // получить роль по юзеру
        // получить все пермисси по ролям
        // добвить все пермиссии в клаймс
        
        var signingCredentials = new SigningCredentials( 
            new SymmetricSecurityKey( Encoding.UTF8.GetBytes(_options.SecretKey)), 
            SecurityAlgorithms.HmacSha256 );
        var claims = new List<Claim>
        {
            new Claim("UserId", user.Id.ToString())
        };
       
        foreach (var permission in permissions)
        {
            claims.Add(new Claim("permission", permission.ToString()));
        }

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHourse));
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }
}