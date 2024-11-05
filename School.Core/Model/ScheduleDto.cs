namespace School.Core.Model;

public class ScheduleDto
{
    public DayOfWeek DayOfWeek { get; set; }
    public int Number { get; set; }
    public string Lesson { get; set; }
    public String GradeLevel { get; set; }
}