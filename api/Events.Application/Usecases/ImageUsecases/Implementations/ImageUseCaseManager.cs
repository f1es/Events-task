using AutoMapper;
using Events.Application.Usecases.ImageUsecases.Interfaces;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.ImageUsecases.Implementations;

public class ImageUseCaseManager : IImageUseCaseManager
{
	private readonly Lazy<IDeleteImageUseCase> deleteImageUseCase;
	private readonly Lazy<IGetImageUseCase> getImageUseCase;
	private readonly Lazy<IUpdateImageUseCase> updateImageUseCase;
	private readonly Lazy<IUploadImageUseCase> uploadImageUseCase;
	public ImageUseCaseManager(
		IRepositoryManager repositoryManager,
		IMapper mapper)
	{
		deleteImageUseCase = new Lazy<IDeleteImageUseCase>(() =>
		new DeleteImageUseCase(repositoryManager));

		getImageUseCase = new Lazy<IGetImageUseCase>(() =>
		new GetImageUseCase(
			repositoryManager,
			mapper));

		updateImageUseCase = new Lazy<IUpdateImageUseCase>(() =>
		new UpdateImageUseCase(
			repositoryManager,
			mapper));

		uploadImageUseCase = new Lazy<IUploadImageUseCase>(() =>
		new UploadImageUseCase(
			repositoryManager,
			mapper));
	}

	public IDeleteImageUseCase DeleteImageUseCase => deleteImageUseCase.Value;

	public IGetImageUseCase GetImageUseCase => getImageUseCase.Value;

	public IUpdateImageUseCase UpdateImageUseCase => updateImageUseCase.Value;

	public IUploadImageUseCase UploadImageUseCase => uploadImageUseCase.Value;
}
