using School.Application.Interfaces.Auth;

namespace School.Infrastructure;

public class PasswordHasher: IPasswordHasher
{
    public string GenerateHash(string password) => 
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool VerifyHash(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}