namespace School.API.Contracts.Parent;

public record GetParentByIdResponse(Guid Id, string FirstName, string LastName, string MiddleName, string Sex, string Phone);