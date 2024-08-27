using AutoMapper;
using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class EventUseCaseManager : IEventUseCaseManager
{
	private readonly Lazy<ICreateEventUseCase> _createEventUseCase;
	private readonly Lazy<IDeleteEventUseCase> _deleteEventUseCase;
	private readonly Lazy<IGetAllEventsUseCase> _getAllEventsUseCase;
	private readonly Lazy<IGetEventByIdUseCase> _getEventByIdUseCase;
	private readonly Lazy<IUpdateEventUseCase> _updateEventUseCase;

	public EventUseCaseManager(
		IRepositoryManager repositoryManager,
		IMapper mapper)
	{
		_createEventUseCase = new Lazy<ICreateEventUseCase>(() =>
		new CreateEventUseCase(
			repositoryManager,
			mapper));

		_deleteEventUseCase = new Lazy<IDeleteEventUseCase>(() =>
		new DeleteEventUseCase(repositoryManager));

		_getAllEventsUseCase = new Lazy<IGetAllEventsUseCase>(() =>
		new GetAllEventsUseCase(repositoryManager, mapper));

		_getEventByIdUseCase = new Lazy<IGetEventByIdUseCase>(() =>
		new GetEventByIdUseCase(
			repositoryManager,
			mapper));

		_updateEventUseCase = new Lazy<IUpdateEventUseCase>(() =>
		new UpdateEventUseCase(
			repositoryManager,
			mapper));
	}

	public ICreateEventUseCase CreateEventUseCase => _createEventUseCase.Value;

	public IDeleteEventUseCase DeleteEventUseCase => _deleteEventUseCase.Value;

	public IGetAllEventsUseCase GetAllEventsUseCase => _getAllEventsUseCase.Value;

	public IGetEventByIdUseCase GetEventByIdUseCase => _getEventByIdUseCase.Value;

	public IUpdateEventUseCase UpdateEventUseCase => _updateEventUseCase.Value;
}
