using AutoMapper;
using Events.Application.Usecases.ImageUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Events.Tests.Usecases.ImageUsecases;

public class UploadImageUseCaseTest
{
	private readonly Mock<IRepositoryManager> _repositoryManagerMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly UploadImageUseCase _uploadImageUseCase;

    public UploadImageUseCaseTest()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _uploadImageUseCase = new UploadImageUseCase(
            _repositoryManagerMock.Object, 
            _mapperMock.Object);
    }

	[Fact]
    public async void UploadImageAsync_ReturnsImageResponseDto()
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
		_mapperMock.Setup(m =>
		m.Map<Image>(It.IsAny<IFormFile>()))
			.Returns(imageModel);

		_repositoryManagerMock.Setup(r =>
		r.Image.Create(imageModel));

		_mapperMock.Setup(m => 
		m.Map<ImageResponseDto>(imageModel))
			.Returns(It.IsAny<ImageResponseDto>());

		// Act
		await _uploadImageUseCase.UploadImageAsync(
			eventId, 
			It.IsAny<IFormFile>(), 
			trackChanges);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

		_mapperMock.Verify(m =>
		m.Map<Image>(It.IsAny<IFormFile>()), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Image.Create(imageModel), Times.Once);

		_mapperMock.Verify(m =>
		m.Map<ImageResponseDto>(imageModel), Times.Once);
	}
}
