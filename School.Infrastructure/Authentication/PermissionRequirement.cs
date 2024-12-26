using Microsoft.AspNetCore.Authorization;
using School.Core.Enums;

namespace School.Infrastructure.Authentication;

public class PermissionRequirement(Permission[] permission) : IAuthorizationRequirement
{
    public Permission[] Permissions { get; set; } = permission;
}