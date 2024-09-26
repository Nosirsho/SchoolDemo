using School.Core.Model;

namespace School.API.Contracts.Teacher;

public record UpdateTeacherRequest(
    Guid Id, 
    string FirstName, 
    string MiddleName, 
    string LastName, 
    DateTime BirthDate, 
    string Phone, 
    Sex Sex);