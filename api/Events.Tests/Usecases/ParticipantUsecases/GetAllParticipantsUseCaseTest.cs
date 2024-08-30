using AutoMapper;
using Events.Application.Usecases.EventUsecases.Implementations;
using Events.Application.Usecases.ParticipantUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;
using Moq;

namespace Events.Tests.Usecases.ParticipantUsecases;

public class GetAllParticipantsUseCaseTest
{
	private readonly Mock<IRepositoryManager> _repositoryManagerMock;
	private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllParticipantsUseCase _getAllParticipantsUseCase;
    public GetAllParticipantsUseCaseTest()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _getAllParticipantsUseCase = new GetAllParticipantsUseCase(
            _repositoryManagerMock.Object, 
            _mapperMock.Object);
    }

    [Fact]
    public async void GetAllParticipantsAsync_ReturnsIEnumerableParticipantResponseDto()
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

		_repositoryManagerMock.Setup(r =>
		r.Participant.GetAllAsync(eventId, It.IsAny<Paging>(), trackChanges))
			.ReturnsAsync(It.IsAny<IEnumerable<Participant>>());

		_mapperMock.Setup(m => 
		m.Map<IEnumerable<ParticipantResponseDto>>(It.IsAny<IEnumerable<Participant>>()))
			.Returns(It.IsAny<IEnumerable<ParticipantResponseDto>>());

		// Act 
		await _getAllParticipantsUseCase.GetAllParticipantsAsync(
			eventId,
			It.IsAny<Paging>(),
			trackChanges);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.GetAllAsync(eventId, It.IsAny<Paging>(), trackChanges), Times.Once);

		_mapperMock.Verify(m =>
		m.Map<IEnumerable<ParticipantResponseDto>>(It.IsAny<IEnumerable<Participant>>()), Times.Once);
	}
}
