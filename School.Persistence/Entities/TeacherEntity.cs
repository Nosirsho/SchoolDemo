using School.Core.Enums;

namespace School.Persistence.Entities;

public class TeacherEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public bool IsDeleted { get; set; }
    
    public Sex Sex { get; set; }

    public GradeLevelEntity? GradeLevel { get; set; }
    public Guid? GradeLevelId { get; set; }
    public IEnumerable<ScheduleEntity> Schedules { get; set; }
}