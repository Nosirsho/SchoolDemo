using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Persistence.Entities;

namespace School.Persistence.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<TeacherEntity>
{
    public void Configure(EntityTypeBuilder<TeacherEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Teachers");

        builder
            .HasOne(t => t.GradeLevel)
            .WithOne(g => g.Teacher);
        
        builder
            .HasMany(t=>t.Schedules)
            .WithOne(s => s.Teacher)
            .HasForeignKey(s => s.TeacherId);
        
    }
}