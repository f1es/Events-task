using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Configuration;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
	public void Configure(EntityTypeBuilder<Image> builder)
	{
		builder.HasKey(i => i.Id);

		builder.Property(i => i.Name)
			.IsRequired();

		builder.Property(i => i.Type)
			.IsRequired();

		builder.Property(i => i.Content)
			.IsRequired();
	}
}
