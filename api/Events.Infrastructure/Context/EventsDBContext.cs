using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Events.Infrastructure.Context;

public class EventsDBContext : DbContext
{
    public EventsDBContext()
    { }  

    public DbSet<Event>? Events { get; set; }
	public DbSet<Participant>? Participants { get; set; }
	public DbSet<User>? Users { get; set; }
	public DbSet<Image>? Images { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
