using AutoMapper;
using Events.Application.Usecases.RefreshTokenUseCase.Interfaces;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.RefreshTokenUseCase.Implementations;

public class UpdateRefreshTokenUseCase : IUpdateRefreshTokenUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;

	public UpdateRefreshTokenUseCase(IRepositoryManager repositoryManager, IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}

	public async Task UpdateRefreshToken(Guid userId, RefreshToken refreshToken, bool trackChanges)
	{
		var userToken = await _repositoryManager.RefreshToken.GetRefreshTokenByUserIdAsync(userId, trackChanges);

		userToken = _mapper.Map(refreshToken, userToken);

		await _repositoryManager.SaveAsync();
	}
}
