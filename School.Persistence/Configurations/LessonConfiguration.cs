using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Persistence.Entities;

namespace School.Persistence.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<LessonEntity>
{
    public void Configure(EntityTypeBuilder<LessonEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Lessons");

        builder.HasMany<ScheduleEntity>(l => l.Schedules) // Один урок может быть во многих расписаниях
            .WithOne(s => s.Lesson)                      // Каждое расписание ссылается на один урок
            .HasForeignKey(s => s.LessonId)              // Внешний ключ в ScheduleEntity
            .OnDelete(DeleteBehavior.Cascade);
    }
}