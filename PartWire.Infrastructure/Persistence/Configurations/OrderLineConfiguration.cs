using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartWire.Domain.Orders;

namespace PartWire.Infrastructure.Persistence.Configurations;

public sealed class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.ToTable("order_lines");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.OrderId).HasColumnName("order_id").IsRequired();
        builder.Property(x => x.QuotationLineId).HasColumnName("quotation_line_id").IsRequired();
        builder.Property(x => x.ItemType).HasColumnName("item_type").HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(x => x.ItemId).HasColumnName("item_id").IsRequired();
        builder.Property(x => x.OrderedQuantity).HasColumnName("ordered_qty").HasPrecision(18, 4).IsRequired();
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price").HasPrecision(18, 4).IsRequired();
        builder.Property(x => x.OrderedAmount).HasColumnName("ordered_amount").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.RemainingDeliveryQuantity).HasColumnName("remaining_delivery_qty").HasPrecision(18, 4).IsRequired();
        builder.Property(x => x.Note).HasColumnName("note").HasMaxLength(1000);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }
}
