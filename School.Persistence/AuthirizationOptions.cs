namespace School.Persistence;

public class AuthorizationOptions
{
    public ICollection<RolePermissions> RolePermissions { get; set; } = [];
}

public class RolePermissions
{
    public string Role { get; set; } = string.Empty;
    public string[] Permissions { get; set; } = [];
}