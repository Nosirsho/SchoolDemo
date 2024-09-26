namespace School.Core.Model;

public class Teacher
{
    public Teacher(string firstName, string middleName, string lastName, string phone, DateTime birthDate, Sex sex)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Phone = phone;
        BirthDate = birthDate;
        Sex = sex;
    }
    
    public Teacher(Guid id, string firstName, string middleName, string lastName, string phone, DateTime birthDate, Sex sex)
    {
        Id = id;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Phone = phone;
        BirthDate = birthDate;
        Sex = sex;
    }
    
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    
    public Sex Sex { get; set; } = Sex.Man;

    public GradeLevel? GradeLevel { get; set; }
    public Guid? GradeLevelId { get; set; }
}