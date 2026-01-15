namespace DV.Shared.Models;

public record DatasetDefinition
{
    public int DatasetDefinitionId { get; init; }
    public string DatasetName { get; init; } = string.Empty;
    public string BriefDescription { get; init; } = string.Empty;
    public string? FullDescription { get; init; }
    public string Principal { get; init; } = string.Empty;
    public bool? Available { get; init; }
    public int DriveMappingID { get; init; }
    public int ProjectID { get; init; }
    public DateTime CreatedOn { get; init; }
    public int CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
    public int? ModifiedBy { get; init; }

    public DatasetDefinition() { }
}
