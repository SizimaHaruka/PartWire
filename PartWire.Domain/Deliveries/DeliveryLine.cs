using PartWire.Domain.Common;
using PartWire.Domain.Projects;

namespace PartWire.Domain.Deliveries;

public sealed class DeliveryLine : AuditableEntity
{
    private DeliveryLine()
    {
        ItemType = ItemType.PurchasePart;
        CheckResult = string.Empty;
    }

    public long DeliveryId { get; private set; }

    public long OrderLineId { get; private set; }

    public long QuotationLineId { get; private set; }

    public ItemType ItemType { get; private set; }

    public long ItemId { get; private set; }

    public decimal DeliveredQuantity { get; private set; }

    public decimal AcceptedQuantity { get; private set; }

    public decimal DifferenceQuantity { get; private set; }

    public string CheckResult { get; private set; }

    public string? Note { get; private set; }

    public DeliveryLine(
        long deliveryId,
        long orderLineId,
        long quotationLineId,
        ItemType itemType,
        long itemId,
        decimal deliveredQuantity,
        decimal acceptedQuantity,
        string checkResult)
    {
        DeliveryId = deliveryId;
        OrderLineId = orderLineId;
        QuotationLineId = quotationLineId;
        ItemType = itemType;
        ItemId = itemId;
        DeliveredQuantity = deliveredQuantity;
        AcceptedQuantity = acceptedQuantity;
        DifferenceQuantity = deliveredQuantity - acceptedQuantity;
        CheckResult = checkResult;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
