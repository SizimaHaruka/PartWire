using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartWire.Domain.Orders;

namespace PartWire.Infrastructure.Persistence.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.QuotationId).HasColumnName("quotation_id").IsRequired();
        builder.Property(x => x.BusinessPartnerId).HasColumnName("business_partner_id").IsRequired();
        builder.Property(x => x.OrderNo).HasColumnName("order_no").HasMaxLength(11).IsRequired();
        builder.Property(x => x.OrderedOn).HasColumnName("ordered_on").IsRequired();
        builder.Property(x => x.TotalAmount).HasColumnName("total_amount").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.PlannedDeliveryDate).HasColumnName("planned_delivery_date");
        builder.Property(x => x.OrderStatus).HasColumnName("order_status").HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(x => x.CancelledOn).HasColumnName("cancelled_on");
        builder.Property(x => x.CancelReason).HasColumnName("cancel_reason").HasMaxLength(500);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasIndex(x => x.OrderNo).IsUnique();
    }
}
