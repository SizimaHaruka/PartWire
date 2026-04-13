using PartWire.Application.Abstractions.Repositories;

namespace PartWire.Application.Projects;

public sealed class GetProjectDetailUseCase
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectDetailUseCase(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public ProjectDetailDto Execute(long projectId)
    {
        var project = _projectRepository.FindById(projectId)
            ?? throw new InvalidOperationException($"Project '{projectId}' was not found.");

        return new ProjectDetailDto
        {
            Id = project.Id,
            ProjectNo = project.ProjectNo,
            ProjectName = project.ProjectName
        };
    }
}
