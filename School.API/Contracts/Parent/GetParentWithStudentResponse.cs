namespace School.API.Contracts.Parent;

public record GetParentWithStudentResponse(Guid Id, string FullName, List<Child> Children);
public record Child(Guid Id, string ChildFullName);