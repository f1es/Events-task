namespace Events.Application.JWT.Interfaces;

public interface IPasswordHasher
{
    string GenerateHash(string password);

    bool VerifyPassword(string password, string passwordHash);
}
