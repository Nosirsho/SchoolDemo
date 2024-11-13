namespace School.Core.Model;

public class ScheduleDto
{
    public DayOfWeek DayOfWeek { get; set; }
    public int Number { get; set; }
    public Guid LessonId { get; set; }
    public String GradeLevel { get; set; }
    public Guid GradeLevelId { get; set; }
}