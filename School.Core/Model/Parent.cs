using School.Core.Enums;

namespace School.Core.Model;

public class Parent
{
    private Parent(Guid id, string firstName, string middleName, string lastName, 
        Sex sex, Guid studentId, string phone)
    {
        Id = id;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Sex = sex;
        StudentId = studentId;
        Phone = phone;
    }
    
    private Parent(string firstName, string middleName, string lastName, 
        Sex sex, Guid studentId, string phone)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Sex = sex;
        StudentId = studentId;
        Phone = phone;
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public Sex Sex { get; set; }
    public string? Phone { get; set; } = string.Empty;
    public Student? Student { get; set; } = null;
    public Guid StudentId { get; set; }

    public static Parent Create(Guid id, string firstName, string middleName, string lastName, 
        Sex sex, Guid studentId, string phone)
    {
        return new Parent(id, firstName, middleName, lastName, sex, studentId, phone);
    }

    public static Parent Create(string firstName, string middleName, string lastName, 
        Sex sex, Guid studentId, string phone)
    {
        return new Parent(firstName, middleName, lastName, sex, studentId, phone);
    }
}