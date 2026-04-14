using PartWire.Domain.Common;

namespace PartWire.Domain.Projects;

public sealed class ProjectItem : AuditableEntity
{
    private ProjectItem()
    {
    }

    public long ProjectId { get; private set; }

    public ItemType ItemType { get; private set; }

    public long ItemId { get; private set; }

    public decimal RequestedQuantity { get; private set; }

    public string? Unit { get; private set; }

    public string? UsageText { get; private set; }

    public DateOnly? RequestedDueDate { get; private set; }

    public string LineStatus { get; private set; } = string.Empty;

    public string? Note { get; private set; }

    public ProjectItem(
        long projectId,
        ItemType itemType,
        long itemId,
        decimal requestedQuantity,
        string lineStatus)
    {
        ProjectId = projectId;
        ItemType = itemType;
        ItemId = itemId;
        RequestedQuantity = requestedQuantity;
        LineStatus = lineStatus;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
