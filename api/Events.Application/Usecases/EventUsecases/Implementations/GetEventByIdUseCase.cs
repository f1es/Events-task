using AutoMapper;
using Events.Application.Usecases.EventUsecases.Extensions;
using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class GetEventByIdUseCase : IGetEventByIdUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
	public GetEventByIdUseCase(
		IRepositoryManager repositoryManager, 
		IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}
	public async Task<EventResponseDto> GetEventByIdAsync(Guid id, bool trackChanges)
	{
		var eventModel = await _repositoryManager.GetEventByIdAndCheckIfExistAsync(id, trackChanges);

		var eventRsponse = _mapper.Map<EventResponseDto>(eventModel);

		return eventRsponse;
	}
}
