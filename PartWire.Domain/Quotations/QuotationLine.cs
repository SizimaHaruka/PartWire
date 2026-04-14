using PartWire.Domain.Common;
using PartWire.Domain.Projects;

namespace PartWire.Domain.Quotations;

public sealed class QuotationLine : AuditableEntity
{
    private QuotationLine()
    {
        ItemType = ItemType.PurchasePart;
    }

    public long QuotationId { get; private set; }

    public long ProjectItemId { get; private set; }

    public ItemType ItemType { get; private set; }

    public long ItemId { get; private set; }

    public decimal QuotedQuantity { get; private set; }

    public decimal QuotedUnitPrice { get; private set; }

    public decimal QuotedAmount { get; private set; }

    public bool IsAdopted { get; private set; }

    public bool IsOrdered { get; private set; }

    public decimal DeliveredQuantity { get; private set; }

    public decimal CheckedQuantity { get; private set; }

    public decimal InvoicedQuantity { get; private set; }

    public bool IsCompleted { get; private set; }

    public string? Note { get; private set; }

    public QuotationLine(
        long quotationId,
        long projectItemId,
        ItemType itemType,
        long itemId,
        decimal quotedQuantity,
        decimal quotedUnitPrice,
        decimal quotedAmount)
    {
        QuotationId = quotationId;
        ProjectItemId = projectItemId;
        ItemType = itemType;
        ItemId = itemId;
        QuotedQuantity = quotedQuantity;
        QuotedUnitPrice = quotedUnitPrice;
        QuotedAmount = quotedAmount;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
