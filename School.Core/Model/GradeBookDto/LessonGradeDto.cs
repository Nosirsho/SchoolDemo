namespace School.Core.Model.GradeBookDto;

public class LessonGradeDto
{
    public Guid LessonId { get; set; }
    public string LessonName { get; set; }
    public int Grade { get; set; }
}