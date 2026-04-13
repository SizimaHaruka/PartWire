using CommunityToolkit.Mvvm.ComponentModel;
using PartWire.Application.Projects;

namespace PartWire.Desktop.ViewModels;

public sealed partial class ProjectDetailViewModel : ObservableObject
{
    public string Heading { get; }

    public string Summary { get; }

    public ProjectDetailViewModel(GetProjectDetailUseCase getProjectDetailUseCase)
    {
        var project = getProjectDetailUseCase.Execute(1);

        Heading = $"{project.ProjectNo} {project.ProjectName}";
        Summary =
            "Project detail flow is connected from Desktop to Application and Infrastructure. " +
            "Next we can replace the stub repository with EF Core.";
    }
}
