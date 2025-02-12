using School.Core.Enums;

namespace School.Persistence.Entities;

public class StudentEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public Sex Sex { get; set; }
    
    public Guid? GradeLevelId { get; set; }
    public GradeLevelEntity? GradeLevel { get; set; }
    
    public IEnumerable<ParentEntity>? Parents { get; set; } = [];
    public bool IsDeleted { get; set; }
}