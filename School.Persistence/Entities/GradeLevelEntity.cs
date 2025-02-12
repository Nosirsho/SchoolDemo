namespace School.Persistence.Entities;

public class GradeLevelEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime? EntryYear { get; set; }
    public List<StudentEntity>? Students { get; set; }
    public TeacherEntity? Teacher { get; set; }
    public ScheduleEntity? Schedule { get; set; }
}