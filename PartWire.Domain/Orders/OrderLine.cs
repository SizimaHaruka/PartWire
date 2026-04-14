using PartWire.Domain.Common;
using PartWire.Domain.Projects;

namespace PartWire.Domain.Orders;

public sealed class OrderLine : AuditableEntity
{
    private OrderLine()
    {
        ItemType = ItemType.PurchasePart;
    }

    public long OrderId { get; private set; }

    public long QuotationLineId { get; private set; }

    public ItemType ItemType { get; private set; }

    public long ItemId { get; private set; }

    public decimal OrderedQuantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal OrderedAmount { get; private set; }

    public decimal RemainingDeliveryQuantity { get; private set; }

    public string? Note { get; private set; }

    public OrderLine(
        long orderId,
        long quotationLineId,
        ItemType itemType,
        long itemId,
        decimal orderedQuantity,
        decimal unitPrice,
        decimal orderedAmount)
    {
        OrderId = orderId;
        QuotationLineId = quotationLineId;
        ItemType = itemType;
        ItemId = itemId;
        OrderedQuantity = orderedQuantity;
        UnitPrice = unitPrice;
        OrderedAmount = orderedAmount;
        RemainingDeliveryQuantity = orderedQuantity;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
