using Events.Domain.Models;

namespace Events.Application.Services.SecurityServices.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);
	Guid GetUserId(string token);
}
