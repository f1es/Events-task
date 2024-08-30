using AutoMapper;
using Events.Application.Usecases.ParticipantUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;
using Moq;

namespace Events.Tests.Usecases.ParticipantUsecases;

public class CreateParticipantUseCaseTest
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateParticipantUseCase _createParticipantUseCase;
    public CreateParticipantUseCaseTest()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _createParticipantUseCase = new CreateParticipantUseCase(
            _repositoryManagerMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async void CreateParticipantAsync_ReturnsParticipantResponseDto()
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

        var userId = Guid.NewGuid();
        var userModel = new User
        {
            Id = userId
        };
        _repositoryManagerMock.Setup(r =>
        r.User.GetByIdAsync(userId, trackChanges))
            .ReturnsAsync(userModel);

        var participant = new Participant();
        _mapperMock.Setup(m => 
        m.Map<Participant>(It.IsAny<ParticipantRequestDto>()))
            .Returns(participant);

        _repositoryManagerMock.Setup(r => 
        r.Participant.Create(participant));

        _mapperMock.Setup(m => 
        m.Map<ParticipantResponseDto>(participant))
            .Returns(It.IsAny<ParticipantResponseDto>());

        // Act
        await _createParticipantUseCase.CreateParticipantAsync(
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
		r.Participant.Create(participant), Times.Once);

        _mapperMock.Verify(m =>
		m.Map<ParticipantResponseDto>(participant), Times.Once);
    }
}
