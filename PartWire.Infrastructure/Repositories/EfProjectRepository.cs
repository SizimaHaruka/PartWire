using Microsoft.EntityFrameworkCore;
using PartWire.Application.Abstractions.Repositories;
using PartWire.Domain.Projects;
using PartWire.Infrastructure.Persistence;

namespace PartWire.Infrastructure.Repositories;

public sealed class EfProjectRepository : IProjectRepository
{
    private readonly PartWireDbContext _dbContext;

    public EfProjectRepository(PartWireDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Project? FindById(long projectId)
    {
        return _dbContext.Projects
            .AsNoTracking()
            .SingleOrDefault(x => x.Id == projectId);
    }
}
