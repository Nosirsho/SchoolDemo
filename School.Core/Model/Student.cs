using System.Security.AccessControl;
using School.Core.Enums;

namespace School.Core.Model;

public class Student
{
    private Student(string firstName, string lastName, string middleName, DateTime birthDate, Sex sex, Guid gradeLevelId)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        BirthDate = birthDate;
        Sex = sex;
        Id = Guid.NewGuid();
        GradeLevelId = gradeLevelId;
    }
    private Student(Guid id, string firstName, string lastName, string middleName, DateTime birthDate, Sex sex, Guid? gradeLevelId)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        BirthDate = birthDate;
        Sex = sex;
        Id = id;
        GradeLevelId = gradeLevelId;
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public Sex Sex { get; set; }
    
    public Guid? GradeLevelId { get; set; }
    public GradeLevel? GradeLevel { get; set; }
    
    public List<Parent>? Parents { get; set; } = [];
    public bool IsDeleted { get; set; }

    public static Student Create(string firstName, string lastName, string middleName, DateTime birthDate, Sex sex, Guid gradeLevelId)
    {
        return new Student(firstName, lastName, middleName, birthDate, sex, gradeLevelId);
    }
    
    public static Student Create(Guid id, string firstName, string lastName, string middleName, DateTime birthDate, Sex sex, Guid? gradeLevelId)
    {
        return new Student(id, firstName, lastName, middleName, birthDate, sex, gradeLevelId);
    }
}