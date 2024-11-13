namespace School.Core.Model;

public record GradeSchedule(string GradeLevel, Guid GradeLevelId, List<DayLesson> DayLessons);