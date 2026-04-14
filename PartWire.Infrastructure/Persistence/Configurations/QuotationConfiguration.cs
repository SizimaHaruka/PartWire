using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartWire.Domain.Quotations;

namespace PartWire.Infrastructure.Persistence.Configurations;

public sealed class QuotationConfiguration : IEntityTypeConfiguration<Quotation>
{
    public void Configure(EntityTypeBuilder<Quotation> builder)
    {
        builder.ToTable("quotations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.ProjectId)
            .HasColumnName("project_id")
            .IsRequired();

        builder.Property(x => x.BusinessPartnerId)
            .HasColumnName("business_partner_id")
            .IsRequired();

        builder.Property(x => x.InternalQuoteNo)
            .HasColumnName("internal_quote_no")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.SupplierQuoteNo)
            .HasColumnName("supplier_quote_no")
            .HasMaxLength(100);

        builder.Property(x => x.QuoteDate)
            .HasColumnName("quote_date")
            .IsRequired();

        builder.Property(x => x.ValidUntil)
            .HasColumnName("valid_until");

        builder.Property(x => x.TotalAmount)
            .HasColumnName("total_amount")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ApprovalStatus)
            .HasColumnName("approval_status")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.AdoptionStatus)
            .HasColumnName("adoption_status")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.RevisionNo)
            .HasColumnName("revision_no")
            .IsRequired();

        builder.Property(x => x.RootQuotationId)
            .HasColumnName("root_quotation_id");

        builder.Property(x => x.Note)
            .HasColumnName("note")
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(x => x.InternalQuoteNo)
            .IsUnique();
    }
}
