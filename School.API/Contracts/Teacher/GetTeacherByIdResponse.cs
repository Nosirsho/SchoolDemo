namespace School.API.Contracts.Teacher;
public record GetTeacherByIdResponse(Guid Id, string FirstName, string LastName, string MiddleName, DateOnly BirthDate, string Sex, string Phone);