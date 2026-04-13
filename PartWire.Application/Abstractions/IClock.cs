namespace PartWire.Application.Abstractions;

public interface IClock
{
    DateTime UtcNow { get; }
}
