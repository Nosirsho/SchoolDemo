namespace School.Core.Model;

public class Parent
{
    public Parent(Guid id, string firstName, string middleName, string lastName, 
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
    
    public Parent(string firstName, string middleName, string lastName, 
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
    public Sex Sex { get; set; } = Sex.Man;
    public string? Phone { get; set; } = string.Empty;
    public Student? Student { get; set; } = null;
    public Guid StudentId { get; set; }
}