using School.Core.Model;

namespace School.API.Contracts.Student;

public record GetStudentsResponse(Guid Id, string FirstName, string LastName, string MiddleName, DateOnly BirthDate, string GradeLevel, string Sex);
public record GetStudentResponse(Guid Id, string FirstName, string LastName, string MiddleName, DateOnly BirthDate, Guid ?GradeLevelId, string Sex);