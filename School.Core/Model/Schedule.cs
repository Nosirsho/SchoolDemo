namespace School.Core.Model;

public class Schedule
{
    public Schedule()
    {
        
    }
    public Schedule(DayOfWeek dayOfWeek, Lesson lesson, Teacher teacher, GradeLevel gradeLevel)
    {
        Id = Guid.NewGuid();
        DayOfWeek = dayOfWeek;
        Lesson = lesson;
        Teacher = teacher;
        GradeLevel = gradeLevel;
    }
    public Schedule(Guid id, DayOfWeek dayOfWeek, Lesson lesson, Teacher teacher, GradeLevel gradeLevel)
    {
        Id = id;
        DayOfWeek = dayOfWeek;
        Lesson = lesson;
        Teacher = teacher;
        GradeLevel = gradeLevel;
    }
    public Guid Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public int Number { get; set; }
    public Lesson Lesson { get; set; }
    public Guid LessonId { get; set; }
    public Teacher Teacher { get; set; }
    public Guid TeacherId { get; set; }
    public GradeLevel GradeLevel { get; set; }
    public Guid GradeLevelId { get; set; }
}