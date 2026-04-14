using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartWire.Domain.Quotations;

namespace PartWire.Infrastructure.Persistence.Configurations;

public sealed class QuotationLineConfiguration : IEntityTypeConfiguration<QuotationLine>
{
    public void Configure(EntityTypeBuilder<QuotationLine> builder)
    {
        builder.ToTable("quotation_lines");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.QuotationId)
            .HasColumnName("quotation_id")
            .IsRequired();

        builder.Property(x => x.ProjectItemId)
            .HasColumnName("project_item_id")
            .IsRequired();

        builder.Property(x => x.ItemType)
            .HasColumnName("item_type")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.ItemId)
            .HasColumnName("item_id")
            .IsRequired();

        builder.Property(x => x.QuotedQuantity)
            .HasColumnName("quoted_qty")
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.QuotedUnitPrice)
            .HasColumnName("quoted_unit_price")
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.QuotedAmount)
            .HasColumnName("quoted_amount")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.IsAdopted)
            .HasColumnName("is_adopted")
            .IsRequired();

        builder.Property(x => x.IsOrdered)
            .HasColumnName("is_ordered")
            .IsRequired();

        builder.Property(x => x.DeliveredQuantity)
            .HasColumnName("delivered_qty")
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.CheckedQuantity)
            .HasColumnName("checked_qty")
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.InvoicedQuantity)
            .HasColumnName("invoiced_qty")
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.IsCompleted)
            .HasColumnName("is_completed")
            .IsRequired();

        builder.Property(x => x.Note)
            .HasColumnName("note")
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();
    }
}
