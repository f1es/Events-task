using AutoMapper;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.ModelServices.Implementations;
using Events.Domain.Models;
using Events.Domain.Shared;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;
using Events.Domain.Shared.Filters;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Events.Tests.Services.ModelServices;

public class EventServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IValidator<EventForCreateRequestDto>> _createValidatorMock;
    private readonly Mock<IValidator<EventForUpdateRequestDto>> _updateValidatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly EventService _eventService;
    public EventServiceTests()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _createValidatorMock = new Mock<IValidator<EventForCreateRequestDto>>();
        _updateValidatorMock = new Mock<IValidator<EventForUpdateRequestDto>>();
        _mapperMock = new Mock<IMapper>();
        _eventService = new EventService(
            _repositoryManagerMock.Object,
            _mapperMock.Object,
            _createValidatorMock.Object,
            _updateValidatorMock.Object);
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

        _repositoryManagerMock.Setup(r => r.Event.GetByIdAsync(eventId, false))
            .ReturnsAsync(eventModel);
        _mapperMock.Setup(m => m.Map<EventResponseDto>(eventModel)).Returns(It.IsAny<EventResponseDto>());

        // Act
        var result = _eventService.GetEventByIdAsync(eventId, false);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(eventId, false), Times.Once());

        _mapperMock.Verify(m =>
        m.Map<EventResponseDto>(eventModel), Times.Once);
    }
    [Fact]
    public async void CreateEventAsync_ReturnsEventResponseDto()
    {
        // Arrange
        var validationResult = new ValidationResult();

        _createValidatorMock.Setup(v =>
        v.ValidateAsync(It.IsAny<EventForCreateRequestDto>(), default)).ReturnsAsync(validationResult);

        _mapperMock.Setup(m => m.Map<Event>(It.IsAny<EventForCreateRequestDto>())).Returns(It.IsAny<Event>());

        _repositoryManagerMock.Setup(r => r.Event.CreateEvent(It.IsAny<Event>()));

        _mapperMock.Setup(m => m.Map<EventResponseDto>(It.IsAny<Event>())).Returns(It.IsAny<EventResponseDto>());

        // Act
        await _eventService.CreateEventAsync(It.IsAny<EventForCreateRequestDto>());

        // Assert
        _createValidatorMock.Verify(v =>
        v.ValidateAsync(It.IsAny<EventForCreateRequestDto>(), default), Times.Once);

        _mapperMock.Verify(v =>
        v.Map<Event>(It.IsAny<EventForCreateRequestDto>()), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Event.CreateEvent(It.IsAny<Event>()), Times.Once);

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

        var validationResult = new ValidationResult();
        _updateValidatorMock.Setup(v =>
        v.ValidateAsync(It.IsAny<EventForUpdateRequestDto>(), default))
            .ReturnsAsync(validationResult);

        _mapperMock.Setup(m =>
        m.Map(It.IsAny<EventForUpdateRequestDto>(), It.IsAny<Event>()))
            .Returns(It.IsAny<Event>());

        _repositoryManagerMock.Setup(r =>
        r.Event.GetByIdAsync(It.IsAny<Guid>(), true))
            .ReturnsAsync(eventModel);

        // Act 
        await _eventService.UpdateEventAsync(
            It.IsAny<Guid>(),
            It.IsAny<EventForUpdateRequestDto>(),
            trackChanges: true);

        // Assert
        _updateValidatorMock.Verify(v =>
        v.ValidateAsync(It.IsAny<EventForUpdateRequestDto>(), default), Times.Once);

        _mapperMock.Verify(m =>
        m.Map(It.IsAny<EventForUpdateRequestDto>(), It.IsAny<Event>()), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(It.IsAny<Guid>(), true), Times.Once);
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

        _repositoryManagerMock.Setup(r =>
        r.Event.GetByIdAsync(It.IsAny<Guid>(), false))
            .ReturnsAsync(eventModel);

        _repositoryManagerMock.Setup(r =>
        r.Event.DeleteEvent(eventModel));

        // Act 
        await _eventService.DeleteEventAsync(eventId, false);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(It.IsAny<Guid>(), false), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Event.DeleteEvent(It.IsAny<Event>()), Times.Once);
    }
    [Fact]
    public async void GetAllEventsAsync_ReturnsIEnumerableEventResponseDto()
    {
        // Arrange 
        _repositoryManagerMock.Setup(r =>
        r.Event.GetAllAsync(It.IsAny<EventFilter>(), It.IsAny<Paging>(), false))
            .ReturnsAsync(It.IsAny<IEnumerable<Event>>());

        _mapperMock.Setup(m =>
        m.Map<IEnumerable<EventResponseDto>>(It.IsAny<IEnumerable<Event>>()));

        // Act 
        await _eventService.GetAllEventsAsync(
            It.IsAny<EventFilter>(),
            It.IsAny<Paging>(),
            false);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetAllAsync(It.IsAny<EventFilter>(), It.IsAny<Paging>(), false), Times.Once);

        _mapperMock.Verify(m =>
        m.Map<IEnumerable<EventResponseDto>>(It.IsAny<IEnumerable<Event>>()), Times.Once);
    }
}
