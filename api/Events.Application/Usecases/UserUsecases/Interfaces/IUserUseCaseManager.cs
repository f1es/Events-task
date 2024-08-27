namespace Events.Application.Usecases.UserUsecases.Interfaces
{
    public interface IUserUseCaseManager
    {
        IGetAllUsersUseCase GetAllUsersUseCase { get; }
        IGetUserByIdUseCase GetUserByIdUseCase { get; }
        IGrantRoleForUserUseCase GrantRoleForUserUseCase { get; }
        ILoginUserUseCase LoginUserUseCase { get; }
        IRegisterUserUseCase RegisterUserUseCase { get; }
    }
}