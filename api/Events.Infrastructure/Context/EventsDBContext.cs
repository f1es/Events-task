using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Context;

public class EventsDBContext : DbContext
{
    public EventsDBContext()
    { }  

    public DbSet<Event>? Events { get; set; }
	public DbSet<Participant>? Participants { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
