namespace PartWire.Domain.Deliveries;

public enum DeliveryStatus
{
    NotDelivered = 1,
    PartiallyDelivered = 2,
    Delivered = 3,
    Checked = 4,
    DifferenceFound = 5
}
