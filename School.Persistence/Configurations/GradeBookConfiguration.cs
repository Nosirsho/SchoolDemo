using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Persistence.Entities;

namespace School.Persistence.Configurations;

public class GradeBookConfiguration : IEntityTypeConfiguration<GradeBookEntity>
{
    public void Configure(EntityTypeBuilder<GradeBookEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("GradeBooks");
    }
}