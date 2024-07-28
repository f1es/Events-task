using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Configuration;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
	public void Configure(EntityTypeBuilder<Event> builder)
	{
		builder.HasKey(e => e.Id);

		builder.Property(e => e.Name)
			.IsRequired()
			.HasMaxLength(250);

		builder.Property(e => e.Description)
			.HasMaxLength(500);

		builder.Property(e => e.Date)
			.IsRequired();

		builder.Property(e => e.Place)
			.IsRequired();

		builder.Property(e => e.MaxParticipantsCount)
			.IsRequired();

		builder.HasMany(e => e.Participants)
			.WithOne(p => p.Event)
			.HasForeignKey(p => p.Id);
	}
}
