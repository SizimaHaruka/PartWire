using PartWire.Application.Abstractions.Repositories;
using PartWire.Application.Projects;
using PartWire.Domain.Projects;

namespace PartWire.Tests;

public sealed class GetProjectDetailUseCaseTests
{
    [Fact]
    public void Execute_ReturnsProjectDetailDto()
    {
        var useCase = new GetProjectDetailUseCase(new FakeProjectRepository());

        var result = useCase.Execute(42);

        Assert.Equal(42, result.Id);
        Assert.Equal("Q0000000042", result.ProjectNo);
        Assert.Equal("Test Project", result.ProjectName);
    }

    private sealed class FakeProjectRepository : IProjectRepository
    {
        public Project? FindById(long projectId)
        {
            var project = new Project($"Q{projectId:D10}", "Test Project");
            project.SetId(projectId);
            return project;
        }
    }
}
