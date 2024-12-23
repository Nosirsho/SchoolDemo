namespace School.API.Contracts.User;

public record CreateUserRequest(string UserName, string Password, string Email);