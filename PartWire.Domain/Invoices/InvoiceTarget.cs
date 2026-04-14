using PartWire.Domain.Common;

namespace PartWire.Domain.Invoices;

public sealed class InvoiceTarget : AuditableEntity
{
    private InvoiceTarget()
    {
    }

    public long InvoiceId { get; private set; }

    public long DeliveryLineId { get; private set; }

    public decimal InvoicedQuantity { get; private set; }

    public decimal InvoicedAmount { get; private set; }

    public InvoiceTarget(
        long invoiceId,
        long deliveryLineId,
        decimal invoicedQuantity,
        decimal invoicedAmount)
    {
        InvoiceId = invoiceId;
        DeliveryLineId = deliveryLineId;
        InvoicedQuantity = invoicedQuantity;
        InvoicedAmount = invoicedAmount;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
