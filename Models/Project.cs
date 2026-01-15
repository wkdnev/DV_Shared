using System.ComponentModel.DataAnnotations;

namespace DV.Shared.Models;

public record Project
{
    [Key]
    public int ProjectId { get; init; }
    
    [Required]
    [MaxLength(50)]
    public string ProjectCode { get; init; } = string.Empty;
    
    [Required]
    [MaxLength(255)]
    public string ProjectName { get; init; } = string.Empty;
    
    [MaxLength(500)]
    public string FolderPath { get; init; } = string.Empty;
    
    [MaxLength(255)]
    public string Principal { get; init; } = string.Empty;
    
    [Required]
    [MaxLength(128)]
    public string SchemaName { get; init; } = string.Empty;
    
    [MaxLength(1000)]
    public string? Description { get; init; }
    
    public DateTime CreatedDate { get; init; }
    
    public bool IsActive { get; init; } = true;

    public Project()
    {
        CreatedDate = DateTime.UtcNow;
    }

    public Project(int projectId, string projectCode, string projectName, string folderPath,
                   string principal, string schemaName, string? description = null,
                   DateTime createdDate = default, bool isActive = true)
    {
        ProjectId = projectId;
        ProjectCode = projectCode;
        ProjectName = projectName;
        FolderPath = folderPath;
        Principal = principal;
        SchemaName = schemaName;
        Description = description;
        CreatedDate = createdDate == default ? DateTime.UtcNow : createdDate;
        IsActive = isActive;
    }
}
