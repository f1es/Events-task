using AutoMapper;
using Events.Domain.Repositories.Interfaces;
using Events.Application.Services.ModelServices.Implementations;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Events.Tests.Services.ModelServices;

public class EventServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly EventService _eventService;
    public EventServiceTests()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _eventService = new EventService(
            _repositoryManagerMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public void GetEventByIdAsync_ReturnsEventResponseDto()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var eventModel = new Event()
        {
            Id = eventId,
        };
        var trackChanges = false;

        _repositoryManagerMock.Setup(r => r.Event.GetByIdAsync(eventId, trackChanges))
            .ReturnsAsync(eventModel);
        _mapperMock.Setup(m => m.Map<EventResponseDto>(eventModel)).Returns(It.IsAny<EventResponseDto>());

        // Act
        var result = _eventService.GetEventByIdAsync(eventId, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(eventId, trackChanges), Times.Once());

        _mapperMock.Verify(m =>
        m.Map<EventResponseDto>(eventModel), Times.Once);
    }
    [Fact]
    public async void CreateEventAsync_ReturnsEventResponseDto()
    {
        // Arrange
        _mapperMock.Setup(m => m.Map<Event>(It.IsAny<EventRequestDto>())).Returns(It.IsAny<Event>());

        _repositoryManagerMock.Setup(r => r.Event.Create(It.IsAny<Event>()));

        _mapperMock.Setup(m => m.Map<EventResponseDto>(It.IsAny<Event>())).Returns(It.IsAny<EventResponseDto>());

        // Act
        await _eventService.CreateEventAsync(It.IsAny<EventRequestDto>());

        // Assert
        _mapperMock.Verify(v =>
        v.Map<Event>(It.IsAny<EventRequestDto>()), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Event.Create(It.IsAny<Event>()), Times.Once);

        _mapperMock.Verify(m =>
        m.Map<EventResponseDto>(It.IsAny<Event>()), Times.Once);
    }
    [Fact]
    public async void UpdateEventAsync_ReturnsVoid()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var eventModel = new Event
        {
            Id = eventId,
        };
        var trackChanges = true;

        _mapperMock.Setup(m =>
        m.Map(It.IsAny<EventRequestDto>(), It.IsAny<Event>()))
            .Returns(It.IsAny<Event>());

        _repositoryManagerMock.Setup(r =>
        r.Event.GetByIdAsync(It.IsAny<Guid>(), trackChanges))
            .ReturnsAsync(eventModel);

        // Act 
        await _eventService.UpdateEventAsync(
            It.IsAny<Guid>(),
            It.IsAny<EventRequestDto>(),
            trackChanges: trackChanges);

        // Assert
        _mapperMock.Verify(m =>
        m.Map(It.IsAny<EventRequestDto>(), It.IsAny<Event>()), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(It.IsAny<Guid>(), trackChanges), Times.Once);
    }
    [Fact]
    public async void DeleteEventAsync_ReturnsVoid()
    {
        // Arrange 
        var eventId = Guid.NewGuid();
        var eventModel = new Event
        {
            Id = eventId,
        };
        var trackChanges = false;

        _repositoryManagerMock.Setup(r =>
        r.Event.GetByIdAsync(It.IsAny<Guid>(), trackChanges))
            .ReturnsAsync(eventModel);

        _repositoryManagerMock.Setup(r =>
        r.Event.Delete(eventModel));

        // Act 
        await _eventService.DeleteEventAsync(eventId, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(It.IsAny<Guid>(), trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Event.Delete(It.IsAny<Event>()), Times.Once);
    }
    [Fact]
    public async void GetAllEventsAsync_ReturnsIEnumerableEventResponseDto()
    {
        // Arrange 
        var trackChanges = false;

        _repositoryManagerMock.Setup(r =>
        r.Event.GetAllAsync(It.IsAny<EventFilter>(), It.IsAny<Paging>(), trackChanges))
            .ReturnsAsync(It.IsAny<IEnumerable<Event>>());

        _mapperMock.Setup(m =>
        m.Map<IEnumerable<EventResponseDto>>(It.IsAny<IEnumerable<Event>>()));

        // Act 
        await _eventService.GetAllEventsAsync(
            It.IsAny<EventFilter>(),
            It.IsAny<Paging>(),
			trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetAllAsync(It.IsAny<EventFilter>(), It.IsAny<Paging>(), trackChanges), Times.Once);

        _mapperMock.Verify(m =>
        m.Map<IEnumerable<EventResponseDto>>(It.IsAny<IEnumerable<Event>>()), Times.Once);
    }
}
