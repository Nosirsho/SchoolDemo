namespace School.API.Contracts.GradeBook;

public record CreateGradeBookRequest(Guid StudentId, DateTime Date, Guid LessonId, int Grade);