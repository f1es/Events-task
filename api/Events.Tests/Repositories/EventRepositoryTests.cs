using Events.Domain.Models;
using Events.Infrastructure.Context;
using Events.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Events.Tests.Repositories;

public class EventRepositoryTests
{
    private EventsDBContext InitContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<EventsDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
        var context = new EventsDBContext(optionsBuilder.Options);
        return context;
    }

    private Event GetTestEvent(Guid id)
    {
        var testEvent = new Event()
        {
            Id = id,
            Name = "event",
            Category = "Cat",
            Date = DateTime.Now,
            Description = "Description",
            Place = "Place",
            MaxParticipantsCount = 1,
        };

        return testEvent;
    }

    [Fact]
    public async void GetByIdAsync_ReturnsEvent()
    {
        // Arrange
        var context = InitContext();
        var repository = new EventRepository(context);
        var eventGuid = Guid.NewGuid();
        var testEvent = GetTestEvent(eventGuid);

        context.Events.Add(testEvent);
        context.SaveChanges();

        // Act
        var result = await repository.GetByIdAsync(eventGuid, false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testEvent.Id, result.Id);
    }

    [Fact]
    public void CreateEvent_ReturnsVoid()
    {
        // Arrange
        var context = InitContext();
        var repository = new EventRepository(context);
        var eventGuid = Guid.NewGuid();
        var testEvent = GetTestEvent(eventGuid);

        // Act
        repository.CreateEvent(testEvent);
        context.SaveChanges();

        // Assert
        Assert.Contains(testEvent, context.Events);
        Assert.Equal(1, context.Events.Count());
    }

    [Fact]
    public void DeleteEvent_ReturnsVoid()
    {
        // Arrange
        var context = InitContext();
        var repository = new EventRepository(context);
        var eventGuid = Guid.NewGuid();
        var testEvent = GetTestEvent(eventGuid);

        context.Events.Add(testEvent);
        context.SaveChanges();

        // Act
        repository.DeleteEvent(testEvent);
        context.SaveChanges();

        // Assert
        Assert.Equal(0, context.Events.Count());
    }
}
