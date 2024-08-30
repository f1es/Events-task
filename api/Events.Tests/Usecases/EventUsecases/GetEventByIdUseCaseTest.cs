using AutoMapper;
using Events.Application.Usecases.EventUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;
using Moq;

namespace Events.Tests.Usecases.EventUsecases;

public class GetEventByIdUseCaseTest
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IMapper> _mapperMock;
	private readonly GetEventByIdUseCase _getEventByIdUseCase;
    public GetEventByIdUseCaseTest()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _getEventByIdUseCase = new GetEventByIdUseCase(
            _repositoryManagerMock.Object, 
            _mapperMock.Object);
    }
    [Fact]
    public async void GetEventByIdAsync_ReturnsEventResponseDto()
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

        _mapperMock.Setup(m =>
        m.Map<EventResponseDto>(eventModel))
            .Returns(It.IsAny<EventResponseDto>());

        // Act 
        await _getEventByIdUseCase.GetEventByIdAsync(eventId, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

        _mapperMock.Verify(m =>
		m.Map<EventResponseDto>(eventModel), Times.Once);
    }
}
