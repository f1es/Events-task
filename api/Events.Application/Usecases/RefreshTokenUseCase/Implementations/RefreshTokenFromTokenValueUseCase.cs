using Events.Application.Usecases.JwtProviderUsecases.Interfaces;
using Events.Application.Usecases.RefreshProviderUsecase.Interfaces;
using Events.Application.Usecases.RefreshTokenUseCase.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.RefreshTokenUseCase.Implementations;

public class RefreshTokenFromTokenValueUseCase : IRefreshTokenFromTokenValueUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IUpdateRefreshTokenUseCase _updateRefreshTokenUseCase;
	private readonly IGenerateRefreshTokenUseCase _generateRefreshTokenUseCase;
	private readonly IGenerateTokenUseCase _generateTokenUseCase;

	public RefreshTokenFromTokenValueUseCase(
		IRepositoryManager repositoryManager,
		IUpdateRefreshTokenUseCase updateRefreshTokenUseCase,
		IGenerateRefreshTokenUseCase generateRefreshTokenUseCase,
		IGenerateTokenUseCase generateTokenUseCase)
	{
		_repositoryManager = repositoryManager;
		_updateRefreshTokenUseCase = updateRefreshTokenUseCase;
		_generateRefreshTokenUseCase = generateRefreshTokenUseCase;
		_generateTokenUseCase = generateTokenUseCase;
	}

	public async Task<(string accessToken, RefreshToken refreshToken)> RefreshTokensFromTokenValue(string refreshTokenValue, bool trackChanges)
	{
		var refreshToken = await _repositoryManager.RefreshToken.GetRefreshTokenByValueAsync(refreshTokenValue, trackChanges);

		if (!ValidateRefreshToken(refreshToken, refreshTokenValue, trackChanges))
		{
			throw new UnauthorizedException("Refresh token is invalid");
		}

		var userId = refreshToken.UserId;

		var newRefreshToken = _generateRefreshTokenUseCase.GenerateToken(userId);
		await _updateRefreshTokenUseCase.UpdateRefreshToken(userId, newRefreshToken, trackChanges);

		var user = await _repositoryManager.User.GetByIdAsync(userId, trackChanges);
		var accessToken = _generateTokenUseCase.GenerateToken(user);

		return (accessToken, newRefreshToken);
	}
	private bool ValidateRefreshToken(
		RefreshToken refreshToken,
		string refreshTokenValue,
		bool trackChanges)
	{
		if (refreshToken is null)
			return false;

		if (!refreshToken.Token.Equals(refreshTokenValue))
			return false;

		if (refreshToken.Expires < DateTime.UtcNow)
			return false;

		return true;
	}
}
