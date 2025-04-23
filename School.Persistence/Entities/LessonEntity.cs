namespace School.Persistence.Entities;

public class LessonEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<ScheduleEntity>? Schedules { get; set; } = [];
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}