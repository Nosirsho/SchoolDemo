using Microsoft.EntityFrameworkCore;
using School.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Core.Constants;
using School.Core.Enums;

namespace School.Persistence.Configurations;

public class SysSettingConfiguration : IEntityTypeConfiguration<SysSettingEntity>
{
    public void Configure(EntityTypeBuilder<SysSettingEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.ToTable("SysSetting");

        builder
            .HasOne(s => s.Type)
            .WithMany(s => s.SysSettings)
            .HasForeignKey(s => s.SysSettingTypeId);
    }
}