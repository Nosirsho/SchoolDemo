using School.Core.Model;

namespace School.API.Contracts.Teacher;

public record CreateTeacherRequest(string FirstName, string LastName, string MiddleName, string Phone, DateTime BirthDate, Sex Sex);