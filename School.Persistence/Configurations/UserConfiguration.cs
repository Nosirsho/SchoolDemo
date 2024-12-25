using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Core.Model;
using School.Persistence.Entities;

namespace School.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasMany(u=>u.Roles)
            .WithMany(r=>r.Users)
            .UsingEntity<UserRoleEntity>(
                l => l.HasOne<RoleEntity>().WithMany().HasForeignKey("RoleId"),
                r=>r.HasOne<UserEntity>().WithMany().HasForeignKey("UserId"));
    }
}