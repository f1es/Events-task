using AutoMapper;
using Events.Application.Usecases.ImageUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Events.Tests.Usecases.ImageUsecases;

public class UpdateImageUseCaseTest
{
	private readonly Mock<IRepositoryManager> _repositoryManagerMock;
	private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateImageUseCase _updateImageUseCase;
    public UpdateImageUseCaseTest()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _updateImageUseCase = new UpdateImageUseCase(
            _repositoryManagerMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async void UpdateImageUseCase_ReturnsVoid()
    {
		// Arrange
		var eventId = Guid.NewGuid();
		var eventModel = new Event
		{
			Id = eventId,
		};
		var trackChanges = true;

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

		_mapperMock.Setup(m =>
		m.Map(It.IsAny<IFormFile>(), imageModel))
			.Returns(imageModel);

		// Act
		await _updateImageUseCase.UpdateImageAsync(eventId, It.IsAny<IFormFile>(), trackChanges);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Image.GetImageAsync(eventId, trackChanges), Times.Once);

		_mapperMock.Verify(m =>
		m.Map(It.IsAny<IFormFile>(), imageModel), Times.Once);
	}
}
