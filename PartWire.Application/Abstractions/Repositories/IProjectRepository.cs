using PartWire.Domain.Projects;

namespace PartWire.Application.Abstractions.Repositories;

public interface IProjectRepository
{
    Project? FindById(long projectId);
}
