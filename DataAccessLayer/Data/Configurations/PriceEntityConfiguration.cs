using Ticketing_system.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ticketing_system.DataAccessLayer.Data.Configurations;

public class PriceEntityConfiguration : IEntityTypeConfiguration<Price>
{
    public void Configure(EntityTypeBuilder<Price> builder)
    {
        builder.ToTable("Prices");

        builder.HasMany(p => p.Seats)
            .WithOne(s => s.Price)
            .HasForeignKey(s => s.PriceId)
            .HasPrincipalKey(p => p.Id);

        builder.Property(p => p.AmountToPay)
            .HasColumnType("money")
            .IsRequired();
    }
}
