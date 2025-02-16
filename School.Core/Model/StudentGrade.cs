namespace School.Core.Model;

public class StudentGrade
{
    public Student Student { get; set; }
    public List<int> Grades { get; set; }
}