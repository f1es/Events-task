using Events.Domain.Models;

namespace Events.Application.JWT.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);
    Guid GetUserId(string token);
}
