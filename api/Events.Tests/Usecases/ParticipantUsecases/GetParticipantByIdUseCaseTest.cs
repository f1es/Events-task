using AutoMapper;
using Events.Application.Usecases.ParticipantUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;
using Moq;

namespace Events.Tests.Usecases.ParticipantUsecases;

public class GetParticipantByIdUseCaseTest
{
	private readonly Mock<IRepositoryManager> _repositoryManagerMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly GetParticipantByIdUseCase _getParticipantByIdUseCase;
	public GetParticipantByIdUseCaseTest()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
		_mapperMock = new Mock<IMapper>();
		_getParticipantByIdUseCase = new GetParticipantByIdUseCase(
			_repositoryManagerMock.Object, 
			_mapperMock.Object);

    }

	[Fact]
	public async void GetParticipantByIdAsync_ReturnsParticipantResponseDto()
	{
		// Arrange
		var trackChanges = false;

		var eventId = Guid.NewGuid();
		var eventModel = new Event
		{
			Id = eventId,
		};
		_repositoryManagerMock.Setup(r =>
		r.Event.GetByIdAsync(eventId, trackChanges))
			.ReturnsAsync(eventModel);

		var participantId = Guid.NewGuid();
		var participantModel = new Participant
		{
			Id = participantId,
		};
		_repositoryManagerMock.Setup(r =>
		r.Participant.GetByIdAsync(participantId, trackChanges))
			.ReturnsAsync(participantModel);

		_mapperMock.Setup(m => 
		m.Map<ParticipantResponseDto>(participantModel))
			.Returns(It.IsAny<ParticipantResponseDto>());

		// Act
		await _getParticipantByIdUseCase.GetParticipantByIdAsync(
			eventId, 
			participantId, 
			trackChanges);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.GetByIdAsync(participantId, trackChanges), Times.Once);

		_mapperMock.Verify(m =>
		m.Map<ParticipantResponseDto>(participantModel), Times.Once);
	}
}
