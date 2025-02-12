namespace School.Core.Model;

public class GradeBook
{
    private GradeBook(
        Guid id,
        DateTime date,
        Lesson lesson,
        Teacher teacher,
        Student student,
        int grade,
        string topic
        )
    {
        Id = id;
        Date = date;
        Lesson = lesson;
        Teacher = teacher;
        Student = student;
        Grade = grade;
        Topic = topic;
    }
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Lesson Lesson { get; set; }
    public Teacher Teacher { get; set; }
    public Student Student { get; set; }
    public int Grade { get; set; }
    public string Topic { get; set; }

    public static GradeBook Create( Guid id,
        DateTime date,
        Lesson lesson,
        Teacher teacher,
        Student student,
        int grade,
        string topic)
    {
        if (grade is < 0 or > 5) throw new ArgumentException("Некорректная оценка!");
        return new GradeBook(id, date, lesson, teacher, student, grade, topic);
    }
}