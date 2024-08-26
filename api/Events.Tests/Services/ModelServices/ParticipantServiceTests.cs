using AutoMapper;
using Events.Domain.Repositories.Interfaces;
using Events.Application.Services.ModelServices.Implementations;
using Events.Application.Services.ModelServices.Interfaces;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Events.Tests.Services.ModelServices;

public class ParticipantServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IParticipantService _participantService;
    public ParticipantServiceTests()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();

        _participantService = new ParticipantService(
            _repositoryManagerMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async void CreateParticipantAsync_ReturnsParticipantResponseDto()
    {
        // Arrange
        var validationResult = new ValidationResult();

        var eventId = Guid.NewGuid();
        var eventModel = new Event
        {
            Id = eventId,
        };
        var trackChanges = false;

        _repositoryManagerMock.Setup(r =>
        r.Event.GetByIdAsync(eventId, trackChanges))
            .ReturnsAsync(eventModel);

        var userId = Guid.NewGuid();
        var userModel = new User
        {
            Id = userId,
        };

        _repositoryManagerMock.Setup(r =>
        r.User.GetByIdAsync(userId, trackChanges))
            .ReturnsAsync(userModel);

        _mapperMock.Setup(m =>
        m.Map<Participant>(It.IsAny<ParticipantRequestDto>()))
            .Returns(It.IsAny<Participant>());

        _repositoryManagerMock.Setup(r =>
        r.Participant.Create(It.IsAny<Participant>()));

        _mapperMock.Setup(m => m.Map<ParticipantResponseDto>(It.IsAny<Participant>()))
            .Returns(It.IsAny<ParticipantResponseDto>());

        // Act
        await _participantService.CreateParticipantAsync(
            eventId,
            userId,
            It.IsAny<ParticipantRequestDto>(),
			trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.User.GetByIdAsync(userId, trackChanges), Times.Once);

        _mapperMock.Verify(m =>
        m.Map<Participant>(It.IsAny<ParticipantRequestDto>()), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Participant.Create(It.IsAny<Participant>()), Times.Once);

        _mapperMock.Verify(m =>
        m.Map<ParticipantResponseDto>(It.IsAny<Participant>()), Times.Once);
    }

    [Fact]
    public async void GetAllPArticipantsAsync_ReturnsIEnumerableParticipantResponseDto()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var eventModel = new Event
        {
            Id = eventId,
        };
        var trackChanges = false;

        _repositoryManagerMock.Setup(r =>
        r.Event.GetByIdAsync(eventId, trackChanges))
            .ReturnsAsync(eventModel);

        _repositoryManagerMock.Setup(r =>
        r.Participant.GetAllAsync(eventId, It.IsAny<Paging>(), trackChanges))
            .ReturnsAsync(It.IsAny<IEnumerable<Participant>>());

        _mapperMock.Setup(m =>
        m.Map<IEnumerable<ParticipantResponseDto>>(It.IsAny<IEnumerable<Participant>>()))
            .Returns(It.IsAny<IEnumerable<ParticipantResponseDto>>());

        // Act
        await _participantService.GetAllParticipantsAsync(eventId, It.IsAny<Paging>(), trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Participant.GetAllAsync(eventId, It.IsAny<Paging>(), trackChanges), Times.Once);

        _mapperMock.Verify(m =>
        m.Map<IEnumerable<ParticipantResponseDto>>(It.IsAny<IEnumerable<Participant>>()), Times.Once);
    }

    [Fact]
    public async void GetParticipantByIdAsync_ReturnsPartisipantResponseDto()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var eventModel = new Event
        {
            Id = eventId,
        };
        var trackChanges = false;

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
        m.Map<ParticipantResponseDto>(It.IsAny<Participant>()))
            .Returns(It.IsAny<ParticipantResponseDto>());

        // Act 
        await _participantService.GetParticipantByIdAsync(eventId, participantId, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Participant.GetByIdAsync(participantId, trackChanges), Times.Once);

        _mapperMock.Verify(m =>
        m.Map<ParticipantResponseDto>(It.IsAny<Participant>()), Times.Once);
    }

    [Fact]
    public async void DeleteParticipantAsync_ReturnsVoid()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var eventModel = new Event
        {
            Id = eventId,
        };
        var trackChanges = false;

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
        await _participantService.DeleteParticipantAsync(eventId, participantId, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Participant.GetByIdAsync(participantId, trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Participant.Delete(participantModel), Times.Once);
    }

    [Fact]
    public async void UpdateParticipantAsync_ReturnsVoid()
    {
        // Arrange
        var participantId = Guid.NewGuid();
        var participantModel = new Participant
        {
            Id = participantId,
        };
        var trackChanges = true;

        _repositoryManagerMock.Setup(r =>
        r.Participant.GetByIdAsync(participantId, trackChanges))
            .ReturnsAsync(participantModel);

        var eventId = Guid.NewGuid();
        var eventModel = new Event
        {
            Id = eventId,
        };

        _repositoryManagerMock.Setup(r =>
        r.Event.GetByIdAsync(eventId, trackChanges))
            .ReturnsAsync(eventModel);

        _mapperMock.Setup(m =>
        m.Map(It.IsAny<ParticipantRequestDto>(), It.IsAny<Participant>()))
            .Returns(It.IsAny<Participant>());

        // Act 
        await _participantService.UpdateParticipantAsync(
            eventId,
            participantId,
            It.IsAny<ParticipantRequestDto>(),
			trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Participant.GetByIdAsync(participantId, trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Participant.GetByIdAsync(participantId, trackChanges), Times.Once);

        _mapperMock.Verify(m =>
        m.Map(It.IsAny<ParticipantRequestDto>(), It.IsAny<Participant>()), Times.Once);
    }
}
