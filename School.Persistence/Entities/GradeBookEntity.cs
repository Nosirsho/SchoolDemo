using School.Core.Model;

namespace School.Persistence.Entities;

public class GradeBookEntity
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public LessonEntity Lesson { get; set; }
    public TeacherEntity Teacher { get; set; }
    public StudentEntity  Student { get; set; }
    public int Grade { get; set; }
    public string Topic { get; set; } = string.Empty;
}