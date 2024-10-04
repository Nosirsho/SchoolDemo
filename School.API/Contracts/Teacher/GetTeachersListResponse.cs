namespace School.API.Contracts.Teacher;

public record GetTeachersListResponse(Guid Id, string FullName, DateOnly BirthDate, string Phone, string Sex);