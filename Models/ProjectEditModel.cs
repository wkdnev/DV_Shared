namespace DV.Shared.Models;

public class ProjectEditModel
{
    public int ProjectId { get; set; }
    public string ProjectCode { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string FolderPath { get; set; } = string.Empty;
    public string Principal { get; set; } = string.Empty;
    public string SchemaName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; } = true;

    public ProjectEditModel()
    {
        CreatedDate = DateTime.UtcNow;
    }

    public static ProjectEditModel FromProject(Project project)
    {
        return new ProjectEditModel
        {
            ProjectId = project.ProjectId,
            ProjectCode = project.ProjectCode,
            ProjectName = project.ProjectName,
            FolderPath = project.FolderPath,
            Principal = project.Principal,
            SchemaName = project.SchemaName,
            Description = project.Description,
            CreatedDate = project.CreatedDate,
            IsActive = project.IsActive
        };
    }

    public Project ToProject()
    {
        return new Project(
            ProjectId,
            ProjectCode,
            ProjectName,
            FolderPath,
            Principal,
            SchemaName,
            Description,
            CreatedDate,
            IsActive
        );
    }
}
