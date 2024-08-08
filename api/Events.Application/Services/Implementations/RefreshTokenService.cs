using AutoMapper;
using Events.Application.JWT.Interfaces;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Models;

namespace Events.Application.Services.Implementations;

public class RefreshTokenService : IRefreshTokenService
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IRefreshProvider _refreshProvider;
	private readonly IMapper _mapper;
    public RefreshTokenService(
		IRepositoryManager repositoryManager, 
		IRefreshProvider refreshProvider,
		IMapper mapper)
    {
        _repositoryManager = repositoryManager;
		_refreshProvider = refreshProvider;
		_mapper = mapper;
    }
    public async Task<bool> CompareTokensAsync(Guid userId, string token, bool trackChanges)
	{
		var userToken = await _repositoryManager.RefreshToken.GetRefreshTokenAsync(userId, trackChanges);

		if (userToken is null)
		{
			throw new NotFoundException($"user with id {userId} doesnt have refresh token");
		}	

		if (userToken.Token.Equals(token))
			return true;
		else
			return false;
	}

	public async Task CreateOrUpdateRefreshToken(Guid userId, RefreshToken refreshToken, bool trackChanges)
	{
		var token = _refreshProvider.GenerateToken(userId);

		var userToken = await _repositoryManager.RefreshToken.GetRefreshTokenAsync(userId, trackChanges);
		if (userToken is null)
		{
			_repositoryManager.RefreshToken.CreateRefreshToken(token);
		}
		else
		{
			_mapper.Map(userToken, refreshToken);
		}

		await _repositoryManager.SaveAsync();
	}

	public async Task CreateRefreshTokenAsync(Guid userId)
	{
		var token = _refreshProvider.GenerateToken(userId);

		_repositoryManager.RefreshToken.CreateRefreshToken(token);

		await _repositoryManager.SaveAsync();
	}

	public Task DeleteRefreshTokenAsync(Guid userId, bool trackChanges)
	{
		throw new NotImplementedException();
	}

	public async Task<RefreshToken> GetRefreshTokenAsync(Guid userId, bool trackChanges)
	{
		var userToken = await _repositoryManager.RefreshToken.GetRefreshTokenAsync(userId, trackChanges);

		if (userToken is null)
		{
			throw new NotFoundException($"user with id {userId} doesnt have refresh token");
		}

		return userToken;
	}

	public async Task<RefreshToken> TryGetRefreshTokenAsync(Guid userId, bool trackChanges)
	{
		var userToken = await _repositoryManager.RefreshToken.GetRefreshTokenAsync(userId, trackChanges);

		return userToken;
	}
}
