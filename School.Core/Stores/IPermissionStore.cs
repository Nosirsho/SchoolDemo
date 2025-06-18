using School.Core.Enums;

namespace School.Core.Stores;

public interface IPermissionStore
{
    Task<HashSet<string>> GetUserPermissions(Guid userId);
}