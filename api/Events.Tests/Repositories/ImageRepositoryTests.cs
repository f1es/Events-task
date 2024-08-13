using Events.Domain.Models;
using Events.Infrastructure.Context;
using Events.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Events.Tests.Repositories;

public class ImageRepositoryTests
{
	private EventsDBContext InitContext()
	{
		var optionsBuilder = new DbContextOptionsBuilder<EventsDBContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString());
		var context = new EventsDBContext(optionsBuilder.Options);
		return context;
	}

	private Image GetTestImage(Guid id)
	{
		var image = new Image()
		{
			Id = id,
			Name = "Test",
			Content = new byte[2],
			Type = "type"
		};

		return image;
	}

	[Fact]
    public void CreateImage_ReturnsVoid()
    {
        // Arrange
		var context = InitContext();
		var repository = new ImageRepository(context);
		var imageGuid = Guid.NewGuid();
		var testImage = GetTestImage(imageGuid);

        // Act
		repository.CreateImage(It.IsAny<Guid>(), testImage);
		context.SaveChanges();

		// Assert
		Assert.Contains(testImage, context.Images);
		Assert.Equal(1, context.Images.Count());
    }
	[Fact]
	public void DeleteImage_ReturnsVoid()
	{
		// Arrange
		var context = InitContext();
		var repository = new ImageRepository(context);
		var imageGuid = Guid.NewGuid();
		var testImage = GetTestImage(imageGuid);

		context.Images.Add(testImage);
		context.SaveChanges();

		// Act
		repository.DeleteImage(testImage);
		context.SaveChanges();

		// Assert
		Assert.Equal(0, context.Images.Count());
		Assert.DoesNotContain(testImage, context.Images);
	}

	[Fact]
	public async void GetImageAsync_ReturnsImage()
	{
		// Arrange
		var context = InitContext();
		var repository = new ImageRepository(context);
		var imageGuid = Guid.NewGuid();
		var testImage = GetTestImage(imageGuid);

		var eventGuid = Guid.NewGuid();
		testImage.EventId = eventGuid;

		context.Images.Add(testImage);
		context.SaveChanges();

		// Act 
		var result = await repository.GetImageAsync(eventGuid, false);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(testImage.Id, result.Id);
	}
}
