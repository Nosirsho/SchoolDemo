namespace School.Core.Model.GradeBookDto;

public class StudentLessonGradeDto
{
    public Guid StudentId { get; set; }
    public string FullName { get; set; }
    public List<LessonGradeDto> LessonGrade { get; set; } = [];
}