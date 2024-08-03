using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Configuration;

public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
	public void Configure(EntityTypeBuilder<Participant> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Name)
			.IsRequired();

		builder.Property(p => p.Surname)
			.IsRequired();

		builder.Property(p => p.Birthday)
			.IsRequired();

		builder.HasOne(p => p.Event)
			.WithMany(e => e.Participants)
			.HasForeignKey(p => p.EventId);

		builder.HasOne(p => p.User)
			.WithMany(u => u.Participants)
			.HasForeignKey(p => p.UserId);
	}
}
