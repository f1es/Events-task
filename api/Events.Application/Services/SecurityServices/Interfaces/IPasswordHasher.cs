namespace Events.Application.Services.SecurityServices.Interfaces;

public interface IPasswordHasher
{
    string GenerateHash(string password);

    bool VerifyPassword(string password, string passwordHash);
}
