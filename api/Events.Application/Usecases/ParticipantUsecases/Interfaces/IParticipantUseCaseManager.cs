namespace Events.Application.Usecases.ParticipantUsecases.Interfaces
{
    public interface IParticipantUseCaseManager
    {
        ICreateParticipantUseCase CreateParticipantUseCase { get; }
        IDeleteParticipantUseCase DeleteParticipantUseCase { get; }
        IGetAllParticipantsUseCase GetAllParticipantsUseCase { get; }
        IGetParticipantByIdUseCase GetParticipantByIdUseCase { get; }
        IUpdateParticipantUseCase UpdateParticipantUseCase { get; }
    }
}