using PartWire.Domain.Common;

namespace PartWire.Domain.Invoices;

public sealed class Invoice : AuditableEntity
{
    private Invoice()
    {
        InvoiceNo = string.Empty;
        InvoiceStatus = InvoiceStatus.Draft;
    }

    public long QuotationId { get; private set; }

    public long BusinessPartnerId { get; private set; }

    public string InvoiceNo { get; private set; }

    public DateOnly InvoicedOn { get; private set; }

    public decimal TotalAmount { get; private set; }

    public string? BillingPeriod { get; private set; }

    public InvoiceStatus InvoiceStatus { get; private set; }

    public string? Note { get; private set; }

    public Invoice(
        long quotationId,
        long businessPartnerId,
        string invoiceNo,
        DateOnly invoicedOn,
        decimal totalAmount)
    {
        QuotationId = quotationId;
        BusinessPartnerId = businessPartnerId;
        InvoiceNo = invoiceNo;
        InvoicedOn = invoicedOn;
        TotalAmount = totalAmount;
        InvoiceStatus = InvoiceStatus.Registered;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
