using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartWire.Domain.Deliveries;

namespace PartWire.Infrastructure.Persistence.Configurations;

public sealed class DeliveryLineConfiguration : IEntityTypeConfiguration<DeliveryLine>
{
    public void Configure(EntityTypeBuilder<DeliveryLine> builder)
    {
        builder.ToTable("delivery_lines");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.DeliveryId).HasColumnName("delivery_id").IsRequired();
        builder.Property(x => x.OrderLineId).HasColumnName("order_line_id").IsRequired();
        builder.Property(x => x.QuotationLineId).HasColumnName("quotation_line_id").IsRequired();
        builder.Property(x => x.ItemType).HasColumnName("item_type").HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(x => x.ItemId).HasColumnName("item_id").IsRequired();
        builder.Property(x => x.DeliveredQuantity).HasColumnName("delivered_qty").HasPrecision(18, 4).IsRequired();
        builder.Property(x => x.AcceptedQuantity).HasColumnName("accepted_qty").HasPrecision(18, 4).IsRequired();
        builder.Property(x => x.DifferenceQuantity).HasColumnName("difference_qty").HasPrecision(18, 4).IsRequired();
        builder.Property(x => x.CheckResult).HasColumnName("check_result").HasMaxLength(30).IsRequired();
        builder.Property(x => x.Note).HasColumnName("note").HasMaxLength(1000);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }
}
