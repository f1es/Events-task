using AutoMapper;
using Events.Application.Usecases.EventUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Moq;

namespace Events.Tests.Usecases.EventUsecases;

public class UpdateEventUseCaseTest
{
	private readonly Mock<IRepositoryManager> _repositoryManagerMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly UpdateEventUseCase _updateEventUseCase;

    public UpdateEventUseCaseTest()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _updateEventUseCase = new UpdateEventUseCase(
            _repositoryManagerMock.Object,
            _mapperMock.Object); 
    }

    [Fact]
    public async void UpdateEventAsync_ReturnsVoid()
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

		_mapperMock.Setup(m =>
		m.Map(It.IsAny<EventRequestDto>(), eventModel))
			.Returns(eventModel);

		// Act
		await _updateEventUseCase.UpdateEventAsync(
			eventId, 
			It.IsAny<EventRequestDto>(),
			trackChanges);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

		_mapperMock.Verify(m =>
		m.Map(It.IsAny<EventRequestDto>(), eventModel), Times.Once);
	}
}
