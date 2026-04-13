using PartWire.Application.Abstractions.Repositories;
using PartWire.Domain.Projects;

namespace PartWire.Infrastructure.Repositories;

public sealed class StubProjectRepository : IProjectRepository
{
    public Project? FindById(long projectId)
    {
        var project = new Project($"Q{projectId:D10}", "Sample Project");
        project.SetId(projectId);
        return project;
    }
}
