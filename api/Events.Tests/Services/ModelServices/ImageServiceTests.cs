using AutoMapper;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.ModelServices.Implementations;
using Events.Application.Services.ModelServices.Interfaces;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Response;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Events.Tests.Services.ModelServices;

public class ImageServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IValidator<IFormFile>> _validatorMock;
    private readonly IImageService _imageService;
    public ImageServiceTests()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _validatorMock = new Mock<IValidator<IFormFile>>();
        _imageService = new ImageService(
            _repositoryManagerMock.Object,
            _mapperMock.Object,
            _validatorMock.Object);
    }

    [Fact]
    public async void UploadImageAsync_ReturnsImageResponseDto()
    {
        // Arrange
        var validationResult = new ValidationResult();
        _validatorMock.Setup(v =>
        v.Validate(It.IsAny<IFormFile>()))
            .Returns(validationResult);

        var eventId = Guid.NewGuid();
        var eventModel = new Event
        {
            Id = eventId,
        };
        var trackChanges = false;

        _repositoryManagerMock.Setup(r =>
        r.Event.GetByIdAsync(eventId, trackChanges))
            .ReturnsAsync(eventModel);

        var image = new Image
        {
            Id = Guid.NewGuid()
        };
        _mapperMock.Setup(m =>
        m.Map<Image>(It.IsAny<IFormFile>()))
            .Returns(image);

        _repositoryManagerMock.Setup(r =>
        r.Image.CreateImage(eventId, It.IsAny<Image>()));

        _mapperMock.Setup(m =>
        m.Map<ImageResponseDto>(It.IsAny<Image>()))
            .Returns(It.IsAny<ImageResponseDto>());

        // Act
        await _imageService.UploadImageAsync(eventId, It.IsAny<IFormFile>(), trackChanges);

        // Assert
        _validatorMock.Verify(v =>
        v.Validate(It.IsAny<IFormFile>()), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

        _mapperMock.Verify(m =>
        m.Map<Image>(It.IsAny<IFormFile>()), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Image.CreateImage(eventId, It.IsAny<Image>()), Times.Once);

        _mapperMock.Verify(m =>
        m.Map<ImageResponseDto>(It.IsAny<Image>()), Times.Once);
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
        var imageModel = new Image()
        {
            Id = imageId,
        };

        _repositoryManagerMock.Setup(r =>
        r.Image.GetImageAsync(eventId, trackChanges))
            .ReturnsAsync(imageModel);

        _mapperMock.Setup(m =>
        m.Map<ImageResponseDto>(It.IsAny<Image>()))
            .Returns(It.IsAny<ImageResponseDto>());

        // Act
        await _imageService.GetImageAsync(eventId, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Image.GetImageAsync(eventId, trackChanges), Times.Once);

        _mapperMock.Verify(m =>
        m.Map<ImageResponseDto>(It.IsAny<Image>()), Times.Once);
    }

    [Fact]
    public async void UpdateImageAsync_ReturnsVoid()
    {
        // Arrange
        var validationResult = new ValidationResult();
        _validatorMock.Setup(v =>
        v.Validate(It.IsAny<IFormFile>()))
            .Returns(validationResult);

        var eventId = Guid.NewGuid();
        var eventModel = new Event
        {
            Id = eventId,
        };
        var trackChanges = true;

        _repositoryManagerMock.Setup(r =>
        r.Event.GetByIdAsync(eventId, trackChanges))
            .ReturnsAsync(eventModel);

        var imageid = Guid.NewGuid();
        var image = new Image 
        { 
            Id = imageid 
        };

        _repositoryManagerMock.Setup(r =>
        r.Image.GetImageAsync(eventId, trackChanges))
            .ReturnsAsync(image);

        _mapperMock.Setup(m =>
        m.Map(It.IsAny<IFormFile>(), It.IsAny<Image>()))
            .Returns(It.IsAny<Image>());

        // Act 
        await _imageService.UpdateImageAsync(eventId, It.IsAny<IFormFile>(), trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
        r.Event.GetByIdAsync(eventId, trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
        r.Image.GetImageAsync(eventId, trackChanges), Times.Once);

        _mapperMock.Verify(m =>
        m.Map(It.IsAny<IFormFile>(), It.IsAny<Image>()), Times.Once);
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
        var image = new Image 
        { 
            Id = imageId ,
            EventId = eventId,
        };
        _repositoryManagerMock.Setup(r => 
        r.Image.GetImageAsync(eventId, trackChanges))
            .ReturnsAsync(image);

        _repositoryManagerMock.Setup(r =>
        r.Image.DeleteImage(image));

        // Act 
        await _imageService.DeleteImageAsync(eventId, trackChanges);

        // Assert
        _repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(It.IsAny<Guid>(), trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
		r.Image.GetImageAsync(It.IsAny<Guid>(), trackChanges), Times.Once);

        _repositoryManagerMock.Verify(r =>
		r.Image.DeleteImage(It.IsAny<Image>()), Times.Once);
    }
}
