namespace School.API.Contracts.GradeBook;

public record DeleteGradeBookRequest(Guid StudentId, DateTime Date, Guid LessonId);