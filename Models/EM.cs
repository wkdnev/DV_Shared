namespace DV.Shared.Models;

/// <summary>
/// Represents an EM (Electronic Management) reference.
/// </summary>
public record EM
{
    public int EMId { get; init; }
    public string? EMName { get; init; }
    public DateTime CreatedOn { get; init; }
    public int CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
    public int? ModifiedBy { get; init; }

    public EM() { }
    public EM(int emId, string? emName, DateTime createdOn, int createdBy, DateTime? modifiedOn, int? modifiedBy)
    {
        EMId = emId;
        EMName = emName;
        CreatedOn = createdOn;
        CreatedBy = createdBy;
        ModifiedOn = modifiedOn;
        ModifiedBy = modifiedBy;
    }
}
