using PartWire.Domain.Common;

namespace PartWire.Domain.Quotations;

public sealed class Quotation : AuditableEntity
{
    private Quotation()
    {
        InternalQuoteNo = string.Empty;
        ApprovalStatus = ApprovalStatus.NotRequested;
        AdoptionStatus = QuoteAdoptionStatus.Draft;
    }

    public long ProjectId { get; private set; }

    public long BusinessPartnerId { get; private set; }

    public string InternalQuoteNo { get; private set; }

    public string? SupplierQuoteNo { get; private set; }

    public DateOnly QuoteDate { get; private set; }

    public DateOnly? ValidUntil { get; private set; }

    public decimal TotalAmount { get; private set; }

    public ApprovalStatus ApprovalStatus { get; private set; }

    public QuoteAdoptionStatus AdoptionStatus { get; private set; }

    public int RevisionNo { get; private set; }

    public long? RootQuotationId { get; private set; }

    public string? Note { get; private set; }

    public Quotation(
        long projectId,
        long businessPartnerId,
        string internalQuoteNo,
        DateOnly quoteDate,
        decimal totalAmount)
    {
        ProjectId = projectId;
        BusinessPartnerId = businessPartnerId;
        InternalQuoteNo = internalQuoteNo;
        QuoteDate = quoteDate;
        TotalAmount = totalAmount;
        ApprovalStatus = ApprovalStatus.NotRequested;
        AdoptionStatus = QuoteAdoptionStatus.Draft;
        RevisionNo = 1;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
