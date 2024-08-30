using AutoMapper;
using Events.Application.Usecases.EventUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;
using Moq;

namespace Events.Tests.Usecases.EventUsecases
{
	public class CreateEventUseCaseTest
	{
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly Mock<IMapper> _mapperMock;
		private readonly CreateEventUseCase _createEventUseCase;

        public CreateEventUseCaseTest()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _mapperMock = new Mock<IMapper>();
            _createEventUseCase = new CreateEventUseCase(
                _repositoryManagerMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async void CreateEventAsync_ReturnsEventResponseDto()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var eventModel = new Event
            {
                Id = eventId,
            };

            _mapperMock.Setup(m =>
            m.Map<Event>(It.IsAny<EventRequestDto>()))
                .Returns(eventModel);

            _repositoryManagerMock.Setup(r => 
            r.Event.Create(eventModel));

            _mapperMock.Setup(m =>
            m.Map<EventResponseDto>(eventModel))
                .Returns(It.IsAny<EventResponseDto>());

            // Act
            await _createEventUseCase.CreateEventAsync(It.IsAny<EventRequestDto>());

            // Assert
            _mapperMock.Verify(m =>
			m.Map<Event>(It.IsAny<EventRequestDto>()), Times.Once);

            _repositoryManagerMock.Verify(r =>
			r.Event.Create(It.IsAny<Event>()), Times.Once);

            _mapperMock.Verify(m =>
			m.Map<EventResponseDto>(It.IsAny<Event>()), Times.Once);
        }

	}
}
