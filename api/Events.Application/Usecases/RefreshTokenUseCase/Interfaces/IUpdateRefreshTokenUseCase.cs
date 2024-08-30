using Events.Domain.Models;

namespace Events.Application.Usecases.RefreshTokenUseCase.Interfaces;

public interface IUpdateRefreshTokenUseCase
{
    Task UpdateRefreshToken(Guid userId, RefreshToken refreshToken, bool trackChanges);
}
