using School.Core.Model;

namespace School.API.Contracts.GradeLevel;

public record UpdateGradeLevelRequest(Guid Id, string Name, DateTime EntryDate);