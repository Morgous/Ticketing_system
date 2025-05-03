using Ticketing_system.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ticketing_system.DataAccessLayer.Data.Configurations;

public class RowEntityConfiguration : IEntityTypeConfiguration<Row>
{
    public void Configure(EntityTypeBuilder<Row> builder)
    {
        builder.ToTable("Rows");

        builder.HasMany(r => r.Seats)
            .WithOne(s => s.Row)
            .HasForeignKey(s => s.RowId)
            .HasPrincipalKey(r => r.Id);

        builder.Property(r => r.Name)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(r => r.SeatCount)
            .IsRequired();
    }
}
