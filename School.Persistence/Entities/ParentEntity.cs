using School.Core.Enums;

namespace School.Persistence.Entities;

public class ParentEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public Sex Sex { get; set; }
    public string? Phone { get; set; } = string.Empty;
    public StudentEntity ? Student { get; set; } = null;
    public Guid StudentId { get; set; }
}