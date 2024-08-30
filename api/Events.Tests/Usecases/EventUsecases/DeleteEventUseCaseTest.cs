using Events.Application.Usecases.EventUsecases.Implementations;
using Events.Domain.Repositories.Interfaces;
using Moq;
using Events.Domain.Models;

namespace Events.Tests.Usecases.EventUsecases
{
	public class DeleteEventUsecaseTest
	{
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
		private readonly DeleteEventUseCase _deleteEventUseCase;
        public DeleteEventUsecaseTest()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _deleteEventUseCase = new DeleteEventUseCase(_repositoryManagerMock.Object);
        }

        [Fact]
        public async void DeleteEventAsync()
        {
            // Arrange
            var trackChanges = false;
            var eventId = Guid.NewGuid();
            var eventModel = new Event
            {
                Id = eventId
            };
            _repositoryManagerMock.Setup(r => 
            r.Event.GetByIdAsync(eventId, trackChanges))
                .ReturnsAsync(eventModel);

            _repositoryManagerMock.Setup(r =>
            r.Event.Delete(eventModel));

            // Act
            await _deleteEventUseCase.DeleteEventAsync(eventId, trackChanges);

            // Assert
            _repositoryManagerMock.Verify(r =>
			r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

            _repositoryManagerMock.Verify(r =>
			r.Event.Delete(eventModel), Times.Once);
        }

	}
}
