using School.Core.Enums;

namespace School.Core.Stores;

public interface IPermissionStore
{
    Task<HashSet<Permission>> GetUserPermissions(Guid userId);
}