using System.Security.Claims;
using School.Core.Enums;
using School.Core.Model;

namespace School.Application.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateJWTToken(User user, HashSet<string> permissions);
}