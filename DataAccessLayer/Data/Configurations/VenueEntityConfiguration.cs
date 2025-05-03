using Ticketing_system.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ticketing_system.DataAccessLayer.Data.Configurations;

public class VenueEntityConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.ToTable("Venues");

        builder.HasMany(v => v.Sections)
            .WithOne(s => s.Venue)
            .HasForeignKey(s => s.VenueId)
            .HasPrincipalKey(v => v.Id);

        builder.HasMany(v => v.Events)
            .WithOne(e => e.Venue)
            .HasForeignKey(e => e.VenueId)
            .HasPrincipalKey(v => v.Id);

        builder.Property(v => v.Title)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(v => v.FullAddress)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(v => v.SectionCount)
            .IsRequired();
    }
}
