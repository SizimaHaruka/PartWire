using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartWire.Domain.Deliveries;

namespace PartWire.Infrastructure.Persistence.Configurations;

public sealed class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("deliveries");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.OrderId).HasColumnName("order_id").IsRequired();
        builder.Property(x => x.DeliveryNoteNo).HasColumnName("delivery_note_no").HasMaxLength(100).IsRequired();
        builder.Property(x => x.DeliveredOn).HasColumnName("delivered_on").IsRequired();
        builder.Property(x => x.DeliveryStatus).HasColumnName("delivery_status").HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(x => x.CheckedOn).HasColumnName("checked_on");
        builder.Property(x => x.CheckedByUserId).HasColumnName("checked_by_user_id");
        builder.Property(x => x.InvoiceTargetMonth).HasColumnName("invoice_target_month").HasMaxLength(7);
        builder.Property(x => x.Note).HasColumnName("note").HasMaxLength(1000);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }
}
