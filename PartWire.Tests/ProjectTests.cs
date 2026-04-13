using PartWire.Domain.Projects;

namespace PartWire.Tests;

public sealed class ProjectTests
{
    [Fact]
    public void Rename_UpdatesProjectName()
    {
        var project = new Project("Q0000000001", "Before");

        project.Rename("After");

        Assert.Equal("After", project.ProjectName);
    }

    [Fact]
    public void Constructor_SetsProjectNumber()
    {
        var project = new Project("Q0000000001", "Project");

        Assert.Equal("Q0000000001", project.ProjectNo);
    }
}
