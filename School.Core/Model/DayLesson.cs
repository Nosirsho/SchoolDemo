namespace School.Core.Model;

public record DayLesson(DayOfWeek dayInt, string DayString, List<LessonNumber> LessonNumbers);
