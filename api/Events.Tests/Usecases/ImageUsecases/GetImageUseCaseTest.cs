using AutoMapper;
using Events.Application.Usecases.ImageUsecases.Implementations;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;
using Moq;

namespace Events.Tests.Usecases.ImageUsecases;

public class GetImageUseCaseTest
{
	private readonly Mock<IRepositoryManager> _repositoryManagerMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly GetImageUseCase _getImageUseCase;
    public GetImageUseCaseTest()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _getImageUseCase = new GetImageUseCase(
            _repositoryManagerMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async void GetImageAsync_ReturnsImageResponseDto()
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

		_mapperMock.Setup(m =>
		m.Map<ImageResponseDto>(imageModel))
			.Returns(It.IsAny<ImageResponseDto>());

		// Act
		await _getImageUseCase.GetImageAsync(eventId, trackChanges);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Image.GetImageAsync(eventId, trackChanges), Times.Once);

		_mapperMock.Verify(m =>
		m.Map<ImageResponseDto>(imageModel), Times.Once);
	}
}
