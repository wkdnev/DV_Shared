namespace DV.Shared.Models;

/// <summary>
/// Represents a filesystem drive mapping for document storage.
/// </summary>
public record DriveMapping
{
    public int DriveMappingId { get; init; }
    public string? DiskId { get; init; }
    public string ShareName { get; init; } = string.Empty;
    public string DirectoryPath { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime CreatedOn { get; init; }
    public int CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
    public int? ModifiedBy { get; init; }

    public DriveMapping() { }
    public DriveMapping(int driveMappingId, string? diskId, string shareName, string directoryPath,
        string description, DateTime createdOn, int createdBy, DateTime? modifiedOn, int? modifiedBy)
    {
        DriveMappingId = driveMappingId;
        DiskId = diskId;
        ShareName = shareName;
        DirectoryPath = directoryPath;
        Description = description;
        CreatedOn = createdOn;
        CreatedBy = createdBy;
        ModifiedOn = modifiedOn;
        ModifiedBy = modifiedBy;
    }
}
