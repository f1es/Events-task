using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(u => u.Id);

		builder.Property(u => u.Username)
			.HasMaxLength(50)
			.IsRequired();

		builder.Property(u => u.Email)
			.IsRequired();

		builder.Property(u => u.PasswordHash)
			.IsRequired();
	}
}
