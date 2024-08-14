using Events.Application.Services.SecurityServices.Interfaces;

namespace Events.Application.Services.SecurityServices.Implementations;

public class PasswordHasher : IPasswordHasher
{
    public string GenerateHash(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool VerifyPassword(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
}
