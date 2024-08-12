﻿using Events.Domain.Models;
using Events.Infrastructure.Context;
using Events.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Events.Tests.Repositories;

public class ParticipantRepositoryTests
{
    private EventsDBContext InitContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<EventsDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
        var context = new EventsDBContext(optionsBuilder.Options);
        return context;
    }
    private Participant GetTestParticipant(Guid id)
    {
        var testParticipant = new Participant()
        {
            Id = id,
            Birthday = new DateTime(2011, 11, 11),
            Email = "mail@mail.m",
            Name = "TestN",
            RegistrationDate = DateTime.UtcNow,
            Surname = "TestS",
        };

        return testParticipant;
    }
    [Fact]
    public async void GetByIdAsync_ReturnsParticipant()
    {
        // Arrange 
        var context = InitContext();
        var repository = new ParticipantRepository(context);
        var participantGuid = Guid.NewGuid();
        var testParticipant = GetTestParticipant(participantGuid);

        context.Participants.Add(testParticipant);
        context.SaveChanges();

        // Act 
        var result = await repository.GetByIdAsync(participantGuid, false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(participantGuid, result.Id);
    }

    [Fact]
    public void CreateParticipant_ReturnsVoid()
    {
        // Arrange
        var context = InitContext();
        var repository = new ParticipantRepository(context);
        var participantGuid = Guid.NewGuid();
        var testParticipant = GetTestParticipant(participantGuid);

        // Act 
        repository.CreateParticipant(new Guid(), new Guid(), testParticipant);
        context.SaveChanges();

        // Assert
        Assert.Contains(testParticipant, context.Participants);
        Assert.Equal(1, context.Participants.Count());
    }
    [Fact]
    public void DeleteParticipant_ReturnsVoid()
    {
        // Arrange
        var context = InitContext();
        var repository = new ParticipantRepository(context);
        var participantGuid = Guid.NewGuid();
        var testParticipant = GetTestParticipant(participantGuid);

        context.Participants.Add(testParticipant);
        context.SaveChanges();

        // Act
        repository.DeleteParticipant(testParticipant);
        context.SaveChanges();

        // Assert
        Assert.Equal(0, context.Participants.Count());
    }

    [Fact]
    public void UpdateParticipant_ReturnsVoid()
    {
        // Arrange
        var context = InitContext();
        var repository = new ParticipantRepository(context);
        var participantGuid = Guid.NewGuid();
        var testParticipant = GetTestParticipant(participantGuid);

        context.Participants.Add(testParticipant);
        context.SaveChanges();

        var newName = "updateTestParticipant";
        testParticipant.Name = newName;

        // Act
        repository.UpdateParticipant(testParticipant);
        context.SaveChanges();

        // Assert
        Assert.Equal(newName, context.Participants.First().Name);
        Assert.Equal(testParticipant.Id, context.Participants.First().Id);
    }
}
