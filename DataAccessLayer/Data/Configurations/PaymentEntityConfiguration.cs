using Ticketing_system.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ticketing_system.DataAccessLayer.Data.Configurations;

public class PaymentEntityConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasMany(x => x.EventSeats)
            .WithMany(x => x.Payments);

        builder.HasKey(x => x.Id);

        builder.Property(p => p.State)
            .HasConversion<int>();
    }
}
