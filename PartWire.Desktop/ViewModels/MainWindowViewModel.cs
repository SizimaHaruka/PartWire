using CommunityToolkit.Mvvm.ComponentModel;
using PartWire.Application.Projects;

namespace PartWire.Desktop.ViewModels;

public sealed partial class MainWindowViewModel : ObservableObject
{
    public string Title => "PartWire";

    public string Description { get; }

    public ProjectDetailViewModel ProjectDetail { get; }

    public MainWindowViewModel(GetProjectDetailUseCase getProjectDetailUseCase)
    {
        Description = "Desktop shell is ready. The first use case is now wired into the screen.";
        ProjectDetail = new ProjectDetailViewModel(getProjectDetailUseCase);
    }
}
