using AutoMapper;
using Events.Application.Usecases.UserUsecases.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.UserUsecases.Implementations;

public class GetUserByIdUseCase : IGetUserByIdUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;

	public GetUserByIdUseCase(
		IRepositoryManager repositoryManager, 
		IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}

	public async Task<UserResponseDto> GetUserByIdAsync(Guid id, bool trackChanges)
	{
		var user = await _repositoryManager.User.GetByIdAsync(id, trackChanges);
		if (user is null)
		{
			throw new NotFoundException($"user with id {id} not found");
		}

		var userResponse = _mapper.Map<UserResponseDto>(user);

		return userResponse;
	}
}
