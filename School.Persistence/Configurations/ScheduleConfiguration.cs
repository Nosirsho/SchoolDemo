using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Persistence.Entities;

namespace School.Persistence.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<ScheduleEntity>
{
    public void Configure(EntityTypeBuilder<ScheduleEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Schedules");

        builder
            .HasOne(s => s.Teacher)
            .WithMany(t => t.Schedules)
            .HasForeignKey(s => s.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(s => s.Lesson)
            .WithMany(l => l.Schedules)
            .HasForeignKey(s => s.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(s => s.GradeLevel)
            .WithOne(g => g.Schedule)
            .HasForeignKey<ScheduleEntity>(s => s.GradeLevelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}