using Microsoft.AspNetCore.Authorization;
using School.Core.Enums;

namespace School.Infrastructure.Authentication;

public class PermissionRequirement(string[] permission) : IAuthorizationRequirement
{
    public string[] Permissions { get; set; } = permission;
}