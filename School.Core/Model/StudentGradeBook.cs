namespace School.Core.Model;

public class StudentGradeBook
{
    public Guid StudentId { get; set; }
    public string StudentFullName { get; set; } 
    public List<GradeBookDay> Grades { get; set; }
}