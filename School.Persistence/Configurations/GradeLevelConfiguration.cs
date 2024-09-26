using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Core.Model;

namespace School.Persistence.Configurations;

public class GradeLevelConfiguration : IEntityTypeConfiguration<GradeLevel>
{
    public void Configure(EntityTypeBuilder<GradeLevel> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(s => s.Students)
            .WithOne(s => s.GradeLevel)
            .HasForeignKey(s => s.GradeLevelId);
        builder
            .HasOne(g => g.Teacher)
            .WithOne(t => t.GradeLevel);
    }
}