using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Core.Enums;
using School.Persistence.Entities;

namespace School.Persistence.Configurations;

public class RolePermissionConffiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
    private readonly AuthorizationOptions _authorization;

    public RolePermissionConffiguration(AuthorizationOptions authorization)
    {
        _authorization = authorization;
    }
    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });
        builder.HasData(ParseRolePermissions());
    }

    private RolePermissionEntity[] ParseRolePermissions()
    {
        return _authorization.RolePermissions
            .SelectMany(rp=>rp.Permissions
                .Select(p=>new RolePermissionEntity
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                })).ToArray();
    }
}