using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
	public void Configure(EntityTypeBuilder<RefreshToken> builder)
	{
		builder.HasKey(rt => rt.Id);

		builder.Property(rt => rt.Token)
			.IsRequired();

		builder.Property(rt => rt.Created)
			.IsRequired();

		builder.Property(rt => rt.Expires)
			.IsRequired();
	}

}
