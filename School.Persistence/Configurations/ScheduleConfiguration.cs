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
            .HasForeignKey(s => s.TeacherId);
        builder
            .HasOne(s=>s.Lesson)
            .WithOne(l=>l.Schedule);
        builder
            .HasOne(s=>s.GradeLevel)
            .WithOne(g=>g.Schedule);
    }
}