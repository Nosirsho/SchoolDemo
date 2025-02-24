namespace School.Core.Model;

public class GradeBook
{
    private GradeBook(
        Guid id,
        DateTime date,
        Guid lessonId,
        Guid teacherId,
        Guid studentId,
        int grade,
        string topic
        )
    {
        Id = id;
        Date = date;
        LessonId = lessonId;
        TeacherId = teacherId;
        StudentId = studentId;
        Grade = grade;
        Topic = topic;
    }
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Lesson Lesson { get; set; }
    public Guid LessonId { get; set; }
    public Teacher Teacher { get; set; }
    public Guid TeacherId { get; set; }
    public Student Student { get; set; }
    public Guid StudentId { get; set; }
    public string StudentFullName { get; set; }
    public int Grade { get; set; }
    public string Topic { get; set; }

    public static GradeBook Create( 
        Guid id,
        DateTime date,
        Guid lessonId,
        Guid teacherId,
        Guid studentId,
        int grade,
        string topic)
    {
        if (grade is < 0 or > 5) throw new ArgumentException("Некорректная оценка!");
        return new GradeBook(id, date, lessonId, teacherId, studentId, grade, topic);
    }
}