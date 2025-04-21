namespace School.API.Contracts.GradeLevel;

public record GetGradeLevelResponse(Guid Id, string Name);
public record GetGradeLevelWithEntryYearResponse(Guid Id, string Name, string? Year);