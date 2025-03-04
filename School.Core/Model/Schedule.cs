namespace School.Core.Model;

public class Schedule
{
    //при удалении пустого конструктора программа не запкскается
    public Schedule()
    {
        
    }
    private Schedule(DayOfWeek dayOfWeek, Lesson lesson, Teacher teacher, GradeLevel gradeLevel)
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
    public Schedule(Guid gradeLevelId, DayOfWeek dayOfWeek, Guid lessonId, int number)
    {
        GradeLevelId = gradeLevelId;
        DayOfWeek = dayOfWeek;
        LessonId = lessonId;
        Number = number;
        TeacherId = new Guid("e34b2980-4c84-4e03-a248-7c54052d4bb7");
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
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsActive { get; set; }

    public static Schedule Create(DayOfWeek dayOfWeek, Lesson lesson, Teacher teacher, GradeLevel gradeLevel)
    {
        return new Schedule(dayOfWeek, lesson, teacher, gradeLevel);
    }
    public static Schedule Create(Guid id, DayOfWeek dayOfWeek, Lesson lesson, Teacher teacher, GradeLevel gradeLevel)
    {
        return new Schedule(id, dayOfWeek, lesson, teacher, gradeLevel);
    }
}