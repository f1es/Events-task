using Events.Domain.Models;

namespace Events.Application.Services.SecurityServices.Interfaces;

public interface IRefreshProvider
{
    RefreshToken GenerateToken(Guid userId);
}
