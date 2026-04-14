using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartWire.Domain.Invoices;

namespace PartWire.Infrastructure.Persistence.Configurations;

public sealed class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoices");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.QuotationId).HasColumnName("quotation_id").IsRequired();
        builder.Property(x => x.BusinessPartnerId).HasColumnName("business_partner_id").IsRequired();
        builder.Property(x => x.InvoiceNo).HasColumnName("invoice_no").HasMaxLength(100).IsRequired();
        builder.Property(x => x.InvoicedOn).HasColumnName("invoiced_on").IsRequired();
        builder.Property(x => x.TotalAmount).HasColumnName("total_amount").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.BillingPeriod).HasColumnName("billing_period").HasMaxLength(50);
        builder.Property(x => x.InvoiceStatus).HasColumnName("invoice_status").HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(x => x.Note).HasColumnName("note").HasMaxLength(1000);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }
}
