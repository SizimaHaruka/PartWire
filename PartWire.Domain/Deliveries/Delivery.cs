using PartWire.Domain.Common;

namespace PartWire.Domain.Deliveries;

public sealed class Delivery : AuditableEntity
{
    private Delivery()
    {
        DeliveryNoteNo = string.Empty;
        DeliveryStatus = DeliveryStatus.NotDelivered;
    }

    public long OrderId { get; private set; }

    public string DeliveryNoteNo { get; private set; }

    public DateOnly DeliveredOn { get; private set; }

    public DeliveryStatus DeliveryStatus { get; private set; }

    public DateOnly? CheckedOn { get; private set; }

    public long? CheckedByUserId { get; private set; }

    public string? InvoiceTargetMonth { get; private set; }

    public string? Note { get; private set; }

    public Delivery(
        long orderId,
        string deliveryNoteNo,
        DateOnly deliveredOn,
        DeliveryStatus deliveryStatus)
    {
        OrderId = orderId;
        DeliveryNoteNo = deliveryNoteNo;
        DeliveredOn = deliveredOn;
        DeliveryStatus = deliveryStatus;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
