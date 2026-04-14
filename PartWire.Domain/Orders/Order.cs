using PartWire.Domain.Common;

namespace PartWire.Domain.Orders;

public sealed class Order : AuditableEntity
{
    private Order()
    {
        OrderNo = string.Empty;
        OrderStatus = OrderStatus.NotOrdered;
    }

    public long QuotationId { get; private set; }

    public long BusinessPartnerId { get; private set; }

    public string OrderNo { get; private set; }

    public DateOnly OrderedOn { get; private set; }

    public decimal TotalAmount { get; private set; }

    public DateOnly? PlannedDeliveryDate { get; private set; }

    public OrderStatus OrderStatus { get; private set; }

    public DateOnly? CancelledOn { get; private set; }

    public string? CancelReason { get; private set; }

    public Order(
        long quotationId,
        long businessPartnerId,
        string orderNo,
        DateOnly orderedOn,
        decimal totalAmount)
    {
        QuotationId = quotationId;
        BusinessPartnerId = businessPartnerId;
        OrderNo = orderNo;
        OrderedOn = orderedOn;
        TotalAmount = totalAmount;
        OrderStatus = OrderStatus.Ordered;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
