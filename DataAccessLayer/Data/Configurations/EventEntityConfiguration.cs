using Ticketing_system.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ticketing_system.DataAccessLayer.Data.Configurations;

public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");

        builder.HasMany(e => e.Seats)
            .WithMany(s => s.Events)
            .UsingEntity<EventSeat>(
                l => l.HasOne<Seat>(e => e.Seat).WithMany(e => e.EventSeats).OnDelete(DeleteBehavior.NoAction),
                r => r.HasOne<Event>(e => e.Event).WithMany(e => e.EventSeats).OnDelete(DeleteBehavior.NoAction));

        builder.Property(ev => ev.Title)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(ev => ev.Description)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(ev => ev.EventDate)
            .IsRequired();
    }
}
