using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Events.Infrastructure.Context;

public class EventsDBContext : DbContext
{
	private readonly IConfiguration _configuration;
    public EventsDBContext()
    { }
    public EventsDBContext(DbContextOptions<EventsDBContext> options) :
		base(options)
    { }
    public EventsDBContext(DbContextOptions<EventsDBContext> options, IConfiguration configuration)
	{
		_configuration = configuration;
	}

    public DbSet<Event>? Events { get; set; }
	public DbSet<Participant>? Participants { get; set; }
	public DbSet<User>? Users { get; set; }
	public DbSet<Image>? Images { get; set; }
	public DbSet<RefreshToken>? RefreshTokens { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			var connectionString = _configuration.GetConnectionString("sqlConnection");
			optionsBuilder.UseSqlServer(connectionString);

		}
		base.OnConfiguring(optionsBuilder);
	}
}
