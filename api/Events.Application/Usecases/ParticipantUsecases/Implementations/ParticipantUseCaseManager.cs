using AutoMapper;
using Events.Application.Usecases.ParticipantUsecases.Interfaces;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.ParticipantUsecases.Implementations;

public class ParticipantUseCaseManager : IParticipantUseCaseManager
{
	private readonly Lazy<ICreateParticipantUseCase> _createParticipantUseCase;
	private readonly Lazy<IDeleteParticipantUseCase> _deleteParticipantUseCase;
	private readonly Lazy<IGetAllParticipantsUseCase> _getAllParticipantsUseCase;
	private readonly Lazy<IGetParticipantByIdUseCase> _getParticipantByIdUseCase;
	private readonly Lazy<IUpdateParticipantUseCase> _updateParticipantUseCase;
	public ParticipantUseCaseManager(
		IRepositoryManager repositoryManager,
		IMapper mapper)
	{
		_createParticipantUseCase = new Lazy<ICreateParticipantUseCase>(() =>
		new CreateParticipantUseCase(
			repositoryManager,
			mapper));

		_deleteParticipantUseCase = new Lazy<IDeleteParticipantUseCase>(() =>
		new DeleteParticipantUseCase(repositoryManager));

		_getAllParticipantsUseCase = new Lazy<IGetAllParticipantsUseCase>(() =>
		new GetAllParticipantsUseCase(
			repositoryManager,
			mapper));

		_getParticipantByIdUseCase = new Lazy<IGetParticipantByIdUseCase>(() =>
		new GetParticipantByIdUseCase(
			repositoryManager,
			mapper));

		_updateParticipantUseCase = new Lazy<IUpdateParticipantUseCase>(() =>
		new UpdateParticipantUseCase(
			repositoryManager,
			mapper));
	}

	public ICreateParticipantUseCase CreateParticipantUseCase => _createParticipantUseCase.Value;

	public IDeleteParticipantUseCase DeleteParticipantUseCase => _deleteParticipantUseCase.Value;

	public IGetAllParticipantsUseCase GetAllParticipantsUseCase => _getAllParticipantsUseCase.Value;

	public IGetParticipantByIdUseCase GetParticipantByIdUseCase => _getParticipantByIdUseCase.Value;

	public IUpdateParticipantUseCase UpdateParticipantUseCase => _updateParticipantUseCase.Value;
}
