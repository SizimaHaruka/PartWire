namespace PartWire.Domain.Orders;

public enum OrderStatus
{
    NotOrdered = 1,
    Ordered = 2,
    PartiallyDelivered = 3,
    Completed = 4,
    Cancelled = 5
}
