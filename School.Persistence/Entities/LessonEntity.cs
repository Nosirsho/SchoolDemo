namespace School.Persistence.Entities;

public class LessonEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ScheduleEntity? Schedule { get; set; }
}