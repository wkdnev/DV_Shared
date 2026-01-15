namespace DV.Shared.Models;

/// <summary>
/// Represents an Old Document Management (DM) system reference.
/// </summary>
public record OldDM
{
    public int OldDMId { get; init; }
    public string? OldDMName { get; init; }
    public DateTime CreatedOn { get; init; }
    public int CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
    public int? ModifiedBy { get; init; }

    public OldDM() { }
    public OldDM(int oldDMId, string? oldDMName, DateTime createdOn, int createdBy, DateTime? modifiedOn, int? modifiedBy)
    {
        OldDMId = oldDMId;
        OldDMName = oldDMName;
        CreatedOn = createdOn;
        CreatedBy = createdBy;
        ModifiedOn = modifiedOn;
        ModifiedBy = modifiedBy;
    }
}
