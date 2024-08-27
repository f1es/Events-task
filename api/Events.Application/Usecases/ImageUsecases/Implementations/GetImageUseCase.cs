using AutoMapper;
using Events.Application.Usecases.ImageUsecases.Extensions;
using Events.Application.Usecases.ImageUsecases.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.ImageUsecases.Implementations;

public class GetImageUseCase : IGetImageUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
	public GetImageUseCase(IRepositoryManager repositoryManager, IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}
	public async Task<ImageResponseDto> GetImageAsync(Guid eventId, bool trackChanges)
	{
		await _repositoryManager.GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

		var image = await _repositoryManager.GetImageByEventIdAndCheckIfExistAsync(eventId, trackChanges);

		var imageResponse = _mapper.Map<ImageResponseDto>(image);

		return imageResponse;
	}
}
