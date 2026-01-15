namespace DV.Shared.Models;

/// <summary>
/// Represents a sheet type classification for documents.
/// </summary>
public record SheetType
{
    public int SheetTypeId { get; init; }
    public string SheetTypeCode { get; init; } = string.Empty;
    public string SheetTypeName { get; init; } = string.Empty;
    public DateTime CreatedOn { get; init; }
    public int CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
    public int? ModifiedBy { get; init; }

    public SheetType() { }
    public SheetType(int sheetTypeId, string sheetTypeCode, string sheetTypeName,
        DateTime createdOn, int createdBy, DateTime? modifiedOn, int? modifiedBy)
    {
        SheetTypeId = sheetTypeId;
        SheetTypeCode = sheetTypeCode;
        SheetTypeName = sheetTypeName;
        CreatedOn = createdOn;
        CreatedBy = createdBy;
        ModifiedOn = modifiedOn;
        ModifiedBy = modifiedBy;
    }
}
