using AutoMapper;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.Implementations;
using Events.Application.Services.Interfaces;
using Events.Domain.Models;
using Events.Domain.Shared;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Events.Tests.Services;

public class ParticipantServiceTests
{
	private readonly Mock<IRepositoryManager> _repositoryManagerMock;
	private readonly Mock<IValidator<ParticipantForCreateRequestDto>> _createValidatorMock;
	private readonly Mock<IValidator<ParticipantForUpdateRequestDto>> _updateValidatorMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly ParticipantService _participantService;
	public ParticipantServiceTests()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
		_mapperMock = new Mock<IMapper>();
		_createValidatorMock = new Mock<IValidator<ParticipantForCreateRequestDto>>();
		_updateValidatorMock = new Mock<IValidator<ParticipantForUpdateRequestDto>>();

		_participantService = new ParticipantService(
			_repositoryManagerMock.Object,
			_mapperMock.Object,
			_updateValidatorMock.Object,
			_createValidatorMock.Object);
	}

    [Fact]
	public async void CreateParticipantAsync_ReturnsParticipantResponseDto()
	{
		// Arrange
		var validationResult = new ValidationResult();

		_createValidatorMock.Setup(v =>
		v.ValidateAsync(It.IsAny<ParticipantForCreateRequestDto>(), default))
			.ReturnsAsync(validationResult);

		var eventId = Guid.NewGuid();
		var eventModel = new Event
		{
			Id = eventId,
		};

		_repositoryManagerMock.Setup(r => 
		r.Event.GetByIdAsync(eventId, false))
			.ReturnsAsync(eventModel);

		var userId = Guid.NewGuid();
		var userModel = new User
		{
			Id = userId,
		};

		_repositoryManagerMock.Setup(r =>
		r.User.GetByIdAsync(userId, false))
			.ReturnsAsync(userModel);

		_mapperMock.Setup(m => 
		m.Map<Participant>(It.IsAny<ParticipantForCreateRequestDto>()))
			.Returns(It.IsAny<Participant>());

		_repositoryManagerMock.Setup(r => 
		r.Participant.CreateParticipant(eventId, userId, It.IsAny<Participant>()));

		_mapperMock.Setup(m => m.Map<ParticipantResponseDto>(It.IsAny<Participant>()))
			.Returns(It.IsAny<ParticipantResponseDto>());

		// Act
		await _participantService.CreateParticipantAsync(
			eventId, 
			userId, 
			It.IsAny<ParticipantForCreateRequestDto>(), 
			false);

		// Assert
		_createValidatorMock.Verify(v =>
		v.ValidateAsync(It.IsAny<ParticipantForCreateRequestDto>(), default), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, false), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.User.GetByIdAsync(userId, false), Times.Once);

		_mapperMock.Verify(m =>
		m.Map<Participant>(It.IsAny<ParticipantForCreateRequestDto>()), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.CreateParticipant(eventId, userId, It.IsAny<Participant>()), Times.Once);

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

		_repositoryManagerMock.Setup(r =>
		r.Event.GetByIdAsync(eventId, false))
			.ReturnsAsync(eventModel);

		_repositoryManagerMock.Setup(r =>
		r.Participant.GetAllAsync(eventId, It.IsAny<Paging>(), false))
			.ReturnsAsync(It.IsAny<IEnumerable<Participant>>());

		_mapperMock.Setup(m =>
		m.Map<IEnumerable<ParticipantResponseDto>>(It.IsAny<IEnumerable<Participant>>()))
			.Returns(It.IsAny<IEnumerable<ParticipantResponseDto>>());

		// Act
		await _participantService.GetAllParticipantsAsync(eventId, It.IsAny<Paging>(), false);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, false), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.GetAllAsync(eventId, It.IsAny<Paging>(), false), Times.Once);

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

		_repositoryManagerMock.Setup(r =>
		r.Event.GetByIdAsync(eventId, false))
			.ReturnsAsync(eventModel);

		var participantId = Guid.NewGuid();
		var participantModel = new Participant
		{
			Id = participantId,
		};

		_repositoryManagerMock.Setup(r => 
		r.Participant.GetByIdAsync(participantId, false))
			.ReturnsAsync(participantModel);

		_mapperMock.Setup(m =>
		m.Map<ParticipantResponseDto>(It.IsAny<Participant>()))
			.Returns(It.IsAny<ParticipantResponseDto>());

		// Act 
		await _participantService.GetParticipantByIdAsync(eventId, participantId, false);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, false), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.GetByIdAsync(participantId, false), Times.Once);

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

		_repositoryManagerMock.Setup(r =>
		r.Event.GetByIdAsync(eventId, false))
			.ReturnsAsync(eventModel);

		var participantId = Guid.NewGuid();
		var participantModel = new Participant
		{
			Id = participantId,
		};

		_repositoryManagerMock.Setup(r =>
		r.Participant.GetByIdAsync(participantId, false))
			.ReturnsAsync(participantModel);

		_repositoryManagerMock.Setup(r =>
		r.Participant.DeleteParticipant(participantModel));

		// Act 
		await _participantService.DeleteParticipantAsync(eventId, participantId, false);

		// Assert
		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, false), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.GetByIdAsync(participantId, false), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.DeleteParticipant(participantModel), Times.Once);	
	}

	[Fact]
	public async void UpdateParticipantAsync_ReturnsVoid()
	{
		// Arrange
		var validationResult = new ValidationResult();

		_updateValidatorMock.Setup(v =>
		v.ValidateAsync(It.IsAny<ParticipantForUpdateRequestDto>(), default))
			.ReturnsAsync(validationResult);

		var participantId = Guid.NewGuid();
		var participantModel = new Participant
		{
			Id = participantId,
		};

		_repositoryManagerMock.Setup(r =>
		r.Participant.GetByIdAsync(participantId, true))
			.ReturnsAsync(participantModel);

		var eventId = Guid.NewGuid();
		var eventModel = new Event
		{
			Id = eventId,
		};

		_repositoryManagerMock.Setup(r =>
		r.Event.GetByIdAsync(eventId, true))
			.ReturnsAsync(eventModel);
		
		_mapperMock.Setup(m => 
		m.Map(It.IsAny<ParticipantForUpdateRequestDto>(), It.IsAny<Participant>()))
			.Returns(It.IsAny<Participant>());

		// Act 
		await _participantService.UpdateParticipantAsync(
			eventId, 
			participantId, 
			It.IsAny<ParticipantForUpdateRequestDto>(), 
			true);

		// Assert
		_updateValidatorMock.Verify(v =>
		v.ValidateAsync(It.IsAny<ParticipantForUpdateRequestDto>(), default), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Event.GetByIdAsync(eventId, true), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.GetByIdAsync(participantId, true), Times.Once);

		_repositoryManagerMock.Verify(r =>
		r.Participant.GetByIdAsync(participantId, true), Times.Once);

		_mapperMock.Verify(m =>
		m.Map(It.IsAny<ParticipantForUpdateRequestDto>(), It.IsAny<Participant>()), Times.Once);
	}
}
