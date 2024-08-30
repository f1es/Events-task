using AutoMapper;
using Events.Application.Usecases.EventUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;
using Moq;

namespace Events.Tests.Usecases.EventUsecases;

public class GetAllEventsUseCaseTest
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IMapper> _mapperMock;
	private readonly GetAllEventsUseCase _getAllEventsUseCase;

    public GetAllEventsUseCaseTest()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _getAllEventsUseCase = new GetAllEventsUseCase(
            _repositoryManagerMock.Object, 
            _mapperMock.Object);
    }

    [Fact]
    public async void GetAllEventsAsync_ReturnsIEnumerableEventResponseDto()
    {
        // Arrange
        var trackChanges = false;
        _repositoryManagerMock.Setup(r =>
        r.Event.GetAllAsync(
            It.IsAny<EventFilter>(),
            It.IsAny<Paging>(),
            trackChanges)).ReturnsAsync(It.IsAny<IEnumerable<Event>>());

        _mapperMock.Setup(m => 
        m.Map<IEnumerable<EventResponseDto>>(It.IsAny<IEnumerable<Event>>()))
            .Returns(It.IsAny<IEnumerable<EventResponseDto>>());

        // Act
        await _getAllEventsUseCase.GetAllEventsAsync(
            It.IsAny<EventFilter>(),
            It.IsAny<Paging>(),
            trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
		r.Event.GetAllAsync(
			It.IsAny<EventFilter>(),
			It.IsAny<Paging>(),
			trackChanges), Times.Once);

        _mapperMock.Verify(m =>
		m.Map<IEnumerable<EventResponseDto>>(It.IsAny<IEnumerable<Event>>()), Times.Once);
    }
}
