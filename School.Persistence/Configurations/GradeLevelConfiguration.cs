using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Persistence.Entities;

namespace School.Persistence.Configurations;

public class GradeLevelConfiguration : IEntityTypeConfiguration<GradeLevelEntity>
{
    public void Configure(EntityTypeBuilder<GradeLevelEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(s => s.Students)
            .WithOne(s => s.GradeLevel)
            .HasForeignKey(s => s.GradeLevelId);
        builder
            .HasOne(g => g.Teacher)
            .WithOne(t => t.GradeLevel);
        builder
            .HasOne(g => g.Schedule)
            .WithOne(s => s.GradeLevel);
    }
}