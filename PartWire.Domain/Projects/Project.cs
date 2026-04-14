using PartWire.Domain.Common;

namespace PartWire.Domain.Projects;

public sealed class Project : AuditableEntity
{
    private Project()
    {
        ProjectNo = string.Empty;
        ProjectName = string.Empty;
    }

    public string ProjectNo { get; private set; }

    public string ProjectName { get; private set; }

    public Project(string projectNo, string projectName)
    {
        ProjectNo = projectNo;
        ProjectName = projectName;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetId(long id)
    {
        Id = id;
    }

    public void Rename(string projectName)
    {
        ProjectName = projectName;
        UpdatedAt = DateTime.UtcNow;
    }
}
