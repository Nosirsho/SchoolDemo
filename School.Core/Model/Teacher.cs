using School.Core.Enums;

namespace School.Core.Model;

public class Teacher
{
    private Teacher(string firstName, string middleName, string lastName, string phone, DateTime birthDate, Sex sex)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Phone = phone;
        BirthDate = birthDate;
        Sex = sex;
    }
    
    private Teacher(Guid id, string firstName, string middleName, string lastName, string phone, DateTime birthDate, Sex sex)
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
    public bool IsDeleted { get; set; }
    
    public Sex Sex { get; set; }

    public GradeLevel? GradeLevel { get; set; }
    public Guid? GradeLevelId { get; set; }

    public IEnumerable<Schedule> Schedules { get; set; }
    
    public static Teacher Create(string firstName, string middleName, string lastName, string phone, DateTime birthDate, Sex sex)
    {
        return new Teacher(firstName, middleName, lastName, phone, birthDate, sex);
    }
    
    public static Teacher Create(Guid id, string firstName, string middleName, string lastName, string phone, DateTime birthDate, Sex sex)
    {
        return new Teacher(id, firstName, middleName, lastName, phone, birthDate, sex);
    }
}