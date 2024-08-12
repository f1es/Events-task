﻿using AutoMapper;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.Implementations;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Response;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Events.Tests.Services;

public class ImageServiceTests
{
	private readonly Mock<IRepositoryManager> _repositoryManagerMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly Mock<IValidator<IFormFile>> _validatorMock;
    private readonly ImageService _imageService;
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

        _repositoryManagerMock.Setup(r =>
        r.Event.GetByIdAsync(eventId, false))
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
        await _imageService.UploadImageAsync(eventId, It.IsAny<IFormFile>(), false);

        // Assert
        _validatorMock.Verify(v =>
		v.Validate(It.IsAny<IFormFile>()), Times.Once);

        _repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, false), Times.Once);

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

		_repositoryManagerMock.Setup(r =>
		r.Event.GetByIdAsync(eventId, false))
			.ReturnsAsync(eventModel);

        var imageId = Guid.NewGuid();
        var imageModel = new Image()
        {
            Id = imageId,
        };

        _repositoryManagerMock.Setup(r => 
        r.Image.GetImageAsync(eventId, false))
            .ReturnsAsync(imageModel);

		_mapperMock.Setup(m =>
		m.Map<ImageResponseDto>(It.IsAny<Image>()))
			.Returns(It.IsAny<ImageResponseDto>());

        // Act
        await _imageService.GetImageAsync(eventId, false);

        // Assert
        _repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, false), Times.Once);

        _repositoryManagerMock.Verify(r => 
        r.Image.GetImageAsync(eventId, false), Times.Once);

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

		_repositoryManagerMock.Setup(r =>
		r.Event.GetByIdAsync(eventId, true))
			.ReturnsAsync(eventModel);

        _repositoryManagerMock.Setup(r =>
        r.Image.GetImageAsync(eventId, true))
            .ReturnsAsync(It.IsAny<Image>());

        _mapperMock.Setup(m => 
        m.Map(It.IsAny<IFormFile>(), It.IsAny<Image>()))
            .Returns(It.IsAny<Image>());

        // Act 
        await _imageService.UpdateImageAsync(eventId, It.IsAny<IFormFile>(), true);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, true), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Image.GetImageAsync(eventId, true), Times.Once);

        _mapperMock.Verify(m =>
		m.Map(It.IsAny<IFormFile>(), It.IsAny<Image>()), Times.Once);
	}
}
