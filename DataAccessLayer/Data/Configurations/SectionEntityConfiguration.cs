using Ticketing_system.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ticketing_system.DataAccessLayer.Data.Configurations;

public class SectionEntityConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("Sections");

        builder.HasMany(s=> s.Rows)
            .WithOne(r => r.Section)
            .HasForeignKey(r => r.SectionId)
            .HasPrincipalKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(s => s.RowCount)
            .IsRequired();
    }
}
