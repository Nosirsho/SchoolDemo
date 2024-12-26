using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Core.Enums;
using School.Persistence.Entities;

namespace School.Persistence.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
   
    public RolePermissionConfiguration()
    {
        
    }
    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });
        builder.HasData(ParseRolePermissions());
    }

    private RolePermissionEntity[] ParseRolePermissions()
    {
        /*
         TODO: 
         Написать логику для подтягивания ролей и пермиссии из конфигуорационного файла
         */
        AuthorizationOptions authorizationOptions = new AuthorizationOptions();
        var rolePermissions1 = new RolePermissions
        {
            Role = "User",
            Permissions = ["Read"],
        };
        var rolePermissions2 = new RolePermissions
        {
            Role = "Admin",
            Permissions = ["Create", "Read", "Update", "Delete"],
        };
        
        authorizationOptions.RolePermissions.Add(rolePermissions1);
        authorizationOptions.RolePermissions.Add(rolePermissions2);
        return authorizationOptions.RolePermissions
            .SelectMany(rp=>rp.Permissions
                .Select(p=>new RolePermissionEntity
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                })).ToArray();
    }
}