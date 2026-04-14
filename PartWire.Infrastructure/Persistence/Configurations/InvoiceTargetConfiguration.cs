using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartWire.Domain.Invoices;

namespace PartWire.Infrastructure.Persistence.Configurations;

public sealed class InvoiceTargetConfiguration : IEntityTypeConfiguration<InvoiceTarget>
{
    public void Configure(EntityTypeBuilder<InvoiceTarget> builder)
    {
        builder.ToTable("invoice_targets");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.InvoiceId).HasColumnName("invoice_id").IsRequired();
        builder.Property(x => x.DeliveryLineId).HasColumnName("delivery_line_id").IsRequired();
        builder.Property(x => x.InvoicedQuantity).HasColumnName("invoiced_qty").HasPrecision(18, 4).IsRequired();
        builder.Property(x => x.InvoicedAmount).HasColumnName("invoiced_amount").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasIndex(x => new { x.InvoiceId, x.DeliveryLineId }).IsUnique();
    }
}
