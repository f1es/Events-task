using AutoMapper;
using Events.Application.Services.SecurityServices.Interfaces;
using Events.Application.Usecases.UserUsecases.Interfaces;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.UserUsecases.Implementations;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
	public GetAllUsersUseCase(
		IRepositoryManager repositoryManager, 
		IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}
	public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(Paging paging, bool trackChanges)
	{
		var users = await _repositoryManager.User.GetAllAsync(paging, trackChanges);

		var usersResponse = _mapper.Map<IEnumerable<UserResponseDto>>(users);

		return usersResponse;
	}
}
