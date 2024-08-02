using Events.Application.Services.Interfaces;

namespace Events.Application.Services.Implementations;

public class PasswordHasher : IPasswordHasher
{
	public string GenerateHash(string password) => 
		BCrypt.Net.BCrypt.EnhancedHashPassword(password);

	public bool VerifyPassword(string password, string passwordHash) => 
		BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
}
