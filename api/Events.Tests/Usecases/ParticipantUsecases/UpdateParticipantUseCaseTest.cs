using AutoMapper;
using Events.Application.Usecases.ParticipantUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Moq;

namespace Events.Tests.Usecases.ParticipantUsecases;

public class UpdateParticipantUseCaseTest
{
	private readonly Mock<IRepositoryManager> _repositoryManagerMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly UpdateParticipantUseCase _updateParticipantUseCase;

    public UpdateParticipantUseCaseTest()
    {
		_repositoryManagerMock = new Mock<IRepositoryManager>();
		_mapperMock = new Mock<IMapper>();
		_updateParticipantUseCase = new UpdateParticipantUseCase(
			_repositoryManagerMock.Object, 
			_mapperMock.Object);
	}

	[Fact]
	public async void UpdateParticipantAsync_ReturnsVoid()
	{
		// Arrange
		var trackChanges = true;

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
		m.Map(It.IsAny<ParticipantRequestDto>(), It.IsAny<Participant>()))
			.Returns(It.IsAny<Participant>);

		// Act
		await _updateParticipantUseCase.UpdateParticipantAsync(
			eventId, 
			participantId, 
			It.IsAny<ParticipantRequestDto>(), 
			trackChanges);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.GetByIdAsync(participantId, trackChanges), Times.Once);

		_mapperMock.Verify(m =>
		m.Map(It.IsAny<ParticipantRequestDto>(), It.IsAny<Participant>()), Times.Once);
	}
}
