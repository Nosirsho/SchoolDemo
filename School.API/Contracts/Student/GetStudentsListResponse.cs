using School.Core.Model;

namespace School.API.Contracts.Student;

public record GetStudentsListResponse(Guid Id, string FullName, DateOnly BirthDate, string GradeLevel, Sex Sex);