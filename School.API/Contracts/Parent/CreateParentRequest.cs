using School.Core.Model;

namespace School.API.Contracts.Parent;

public record CreateParentRequest(string FirstName, string MiddleName, string LastName,
   Sex Sex, Guid StudentId, string Phone);