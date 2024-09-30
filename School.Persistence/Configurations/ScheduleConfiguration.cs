using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Core.Model;

namespace School.Persistence.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(x => x.Id);

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