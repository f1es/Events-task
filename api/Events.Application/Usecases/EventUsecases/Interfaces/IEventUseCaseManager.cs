namespace Events.Application.Usecases.EventUsecases.Interfaces
{
    public interface IEventUseCaseManager
    {
        ICreateEventUseCase CreateEventUseCase { get; }
        IDeleteEventUseCase DeleteEventUseCase { get; }
        IGetAllEventsUseCase GetAllEventsUseCase { get; }
        IGetEventByIdUseCase GetEventByIdUseCase { get; }
        IUpdateEventUseCase UpdateEventUseCase { get; }
    }
}