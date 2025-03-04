namespace School.Persistence.Entities;

public class ScheduleEntity
{
    public Guid Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public int Number { get; set; }
    public LessonEntity Lesson { get; set; }
    public Guid LessonId { get; set; }
    public TeacherEntity Teacher { get; set; }
    public Guid TeacherId { get; set; }
    public GradeLevelEntity GradeLevel { get; set; }
    public Guid GradeLevelId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsActive { get; set; }
}