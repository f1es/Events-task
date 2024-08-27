using Events.Application.Usecases.JwtProviderUsecases.Interfaces;
using Events.Application.Usecases.PasswordHasherUsecases.Interfaces;
using Events.Application.Usecases.RefreshProviderUsecase.Interfaces;
using Events.Application.Usecases.RefreshTokenUseCase.Interfaces;
using Events.Application.Usecases.UserUsecases.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.UserUsecases.Implementations;

public class LoginUserUseCase : ILoginUserUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IVerifyPasswordUseCase _verifyPasswordUseCase;
	private readonly IGenerateTokenUseCase _generateTokenUseCase;
	private readonly IGenerateRefreshTokenUseCase _generateRefreshTokenUseCase;
	private readonly IUpdateRefreshTokenUseCase _updateRefreshTokenUseCase;
	public LoginUserUseCase(
		IRepositoryManager repositoryManager, 
		IVerifyPasswordUseCase verifyPasswordUseCase,
		IGenerateTokenUseCase generateTokenUseCase,
		IGenerateRefreshTokenUseCase generateRefreshTokenUseCase,
		IUpdateRefreshTokenUseCase updateRefreshTokenUseCase)
	{
		_repositoryManager = repositoryManager;
		_verifyPasswordUseCase = verifyPasswordUseCase;
		_generateTokenUseCase = generateTokenUseCase;
		_generateRefreshTokenUseCase = generateRefreshTokenUseCase;
		_updateRefreshTokenUseCase = updateRefreshTokenUseCase;
	}
	public async Task<(string accessToken, RefreshToken refreshToken)> LoginUserAsync(
		UserLoginRequestDto user,
		bool trackUserChanges, 
		bool trackRefreshTokenChanges)
	{
		var userModel = await _repositoryManager.User.GetByUsernameAsync(user.Username, trackUserChanges);
		if (userModel is null)
		{
			throw new UnauthorizedException("Failed to login");
		}

		var verificationResult = _verifyPasswordUseCase.VerifyPassword(user.Password, userModel.PasswordHash);
		if (!verificationResult)
		{
			throw new UnauthorizedException("Failed to login");
		}

		var accessToken = _generateTokenUseCase.GenerateToken(userModel);
		var refreshToken = _generateRefreshTokenUseCase.GenerateToken(userModel.Id);

		await _updateRefreshTokenUseCase.UpdateRefreshToken(
			userModel.Id,
			refreshToken,
			trackRefreshTokenChanges);

		return (accessToken, refreshToken);
	}
}
