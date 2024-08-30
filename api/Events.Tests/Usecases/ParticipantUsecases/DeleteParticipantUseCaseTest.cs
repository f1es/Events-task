using Events.Application.Usecases.ParticipantUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Moq;

namespace Events.Tests.Usecases.ParticipantUsecases;

public class DeleteParticipantUseCaseTest
{
	private readonly Mock<IRepositoryManager> _repositoryManagerMock;
	private readonly DeleteParticipantUseCase _deleteParticipantUseCase;
    public DeleteParticipantUseCaseTest()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _deleteParticipantUseCase = new DeleteParticipantUseCase(_repositoryManagerMock.Object);
    }
	[Fact]
    public async void DeleteParticipantAsync_ReturnsVoid()
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

		_repositoryManagerMock.Setup(r => 
		r.Participant.Delete(participantModel));

		// Act
		await _deleteParticipantUseCase.DeleteParticipantAsync(
			eventId, 
			participantId, 
			trackChanges);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.GetByIdAsync(participantId, trackChanges), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.Delete(participantModel), Times.Once);
	}
}
