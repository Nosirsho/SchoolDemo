using School.Core.Enums;
using School.Core.Stores;

namespace School.Application.Services;

public class PermissionService
{
    private readonly IPermissionStore _permissionStore;

    public PermissionService(IPermissionStore permissionStore)
    {
        _permissionStore = permissionStore;
    }

    public Task<HashSet<Permission>> GetAllPermissionsAsync(Guid userId)
    {
        return _permissionStore.GetUserPermissions(userId);
    }
}