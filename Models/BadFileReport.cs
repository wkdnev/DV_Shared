// ============================================================================
// BadFileReport.cs - Model for Bad File Report Entity
// ============================================================================
namespace DV.Shared.Models;

public record BadFileReport
{
    public int BadFileReportId { get; init; }
    public int ReportedBy { get; init; }
    public DateTime ReportedOn { get; init; }
    public int DocumentPageId { get; init; }
    public string ReportType { get; init; } = string.Empty;
    public bool? ImageStatus { get; init; }
    public string? ImageUrl { get; init; }
    public int UpdatedBy { get; init; }
    public string? CorrectiveAction { get; init; }
    public DateTime CreatedOn { get; init; }
    public int CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
    public int? ModifiedBy { get; init; }

    public BadFileReport() { }
}
