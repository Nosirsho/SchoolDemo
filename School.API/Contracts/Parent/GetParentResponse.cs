using School.Core.Enums;

namespace School.API.Contracts.Parent;

public record GetParentResponse(Guid Id, string FullName, string Sex, string Phone);