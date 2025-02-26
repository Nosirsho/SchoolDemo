using Microsoft.EntityFrameworkCore;
using School.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Core.Constants;
using School.Core.Enums;


namespace School.Persistence.Configurations;

public class SysSettingTypeConfiguration: IEntityTypeConfiguration<SysSettingTypeEntity>
{
    public void Configure(EntityTypeBuilder<SysSettingTypeEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.ToTable("SysSettingType");
        
        var sysSettingTypes = Enum
            .GetValues<SysSettingType>()
            .Select( p => new SysSettingTypeEntity
            {
                Id = BaseConstant.GetByName(p.ToString()),
                Name = p.ToString()
            });
        builder.HasData(sysSettingTypes);
    }
}