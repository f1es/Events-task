namespace Events.Application.Usecases.ImageUsecases.Interfaces
{
    public interface IImageUseCaseManager
    {
        IDeleteImageUseCase DeleteImageUseCase { get; }
        IGetImageUseCase GetImageUseCase { get; }
        IUpdateImageUseCase UpdateImageUseCase { get; }
        IUploadImageUseCase UploadImageUseCase { get; }
    }
}