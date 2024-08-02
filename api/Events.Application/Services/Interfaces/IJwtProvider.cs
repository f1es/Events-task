using Events.Domain.Models;

namespace Events.Application.Services.Interfaces;

public interface IJwtProvider
{
	string GenerateToken(User user);
}
