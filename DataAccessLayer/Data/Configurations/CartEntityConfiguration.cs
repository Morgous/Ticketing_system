using Ticketing_system.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ticketing_system.DataAccessLayer.Data.Configurations;

public class CartEntityConfiguration : IEntityTypeConfiguration<Cart>

{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasMany(x => x.EventSeats)
            .WithMany(x => x.Carts);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CartId)
            .HasColumnName("CartId")
            .IsRequired();
    }
}
