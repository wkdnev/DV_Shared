namespace DV.Shared.Models;

/// <summary>
/// Represents a CM (Content Management) reference.
/// </summary>
public record CM
{
    public int CMId { get; init; }
    public string? CMName { get; init; }
    public DateTime CreatedOn { get; init; }
    public int CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
    public int? ModifiedBy { get; init; }

    public CM() { }
    public CM(int cmId, string? cmName, DateTime createdOn, int createdBy, DateTime? modifiedOn, int? modifiedBy)
    {
        CMId = cmId;
        CMName = cmName;
        CreatedOn = createdOn;
        CreatedBy = createdBy;
        ModifiedOn = modifiedOn;
        ModifiedBy = modifiedBy;
    }
}
