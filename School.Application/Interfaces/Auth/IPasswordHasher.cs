namespace School.Application.Interfaces.Auth;

public interface IPasswordHasher
{
    string GenerateHash(string password);
    bool VerifyHash(string password, string hashedPassword);
}