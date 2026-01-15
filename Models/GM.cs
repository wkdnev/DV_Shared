namespace DV.Shared.Models;

/// <summary>
/// Represents a GM (General Management) reference.
/// </summary>
public record GM
{
    public int GMId { get; init; }
    public string? GMName { get; init; }
    public DateTime CreatedOn { get; init; }
    public int CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
    public int? ModifiedBy { get; init; }

    public GM() { }
    public GM(int gmId, string? gmName, DateTime createdOn, int createdBy, DateTime? modifiedOn, int? modifiedBy)
    {
        GMId = gmId;
        GMName = gmName;
        CreatedOn = createdOn;
        CreatedBy = createdBy;
        ModifiedOn = modifiedOn;
        ModifiedBy = modifiedBy;
    }
}
