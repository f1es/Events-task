using Events.Application.Usecases.ImageUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Moq;

namespace Events.Tests.Usecases.ImageUsecases;

public class DeleteImageUseCaseTest
{
	private readonly Mock<IRepositoryManager> _repositoryManagerMock;
	private readonly DeleteImageUseCase _deleteImageUseCase;

    public DeleteImageUseCaseTest()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _deleteImageUseCase = new DeleteImageUseCase(_repositoryManagerMock.Object);
    }

    [Fact]
    public async void DeleteImageAsync_ReturnsVoid()
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

        var imageId = Guid.NewGuid();
        var imageModel = new Image
        {
            Id = imageId,
        };
        _repositoryManagerMock.Setup(r => 
        r.Image.GetImageAsync(eventId, trackChanges))
            .ReturnsAsync(imageModel);

        _repositoryManagerMock.Setup(r =>
        r.Image.Delete(imageModel));

        // Act
        await _deleteImageUseCase.DeleteImageAsync(eventId, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
		r.Image.GetImageAsync(eventId, trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
		r.Image.Delete(imageModel), Times.Once);
    }
}
