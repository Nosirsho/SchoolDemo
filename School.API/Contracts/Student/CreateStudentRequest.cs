using School.Core.Model;

namespace School.API.Contracts.Student;

public record CreateStudentRequest(string FirstName, string LastName, string MiddleName, DateTime BirthDate, Sex Sex);