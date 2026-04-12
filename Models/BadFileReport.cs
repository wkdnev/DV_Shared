// ============================================================================
// BadFileReport.cs - Model for Bad File Report Entity
// ============================================================================
//
// Purpose: Represents a report of a bad/poor quality file or image.
// ReadOnly and Editor users can create reports; only Editors can resolve them.
//
// Status workflow: Open → InProgress → Resolved/Closed/Rejected
// ============================================================================
namespace DV.Shared.Models;

public record BadFileReport
{
    // ── Identity ──
    public int BadFileReportId { get; init; }

    // ── Reporter context ──
    public int ReportedBy { get; init; }
    public DateTime ReportedOn { get; init; }

    // ── Document context ──
    public int DocumentPageId { get; init; }
    public int DocumentId { get; init; }
    public string SchemaName { get; init; } = string.Empty;
    public string? FileName { get; init; }
    public int PageNumber { get; init; }

    // ── Report details ──
    public string ReportType { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Priority { get; init; } = "Normal";
    public string Status { get; init; } = "Open";

    // ── Legacy fields (widened) ──
    public bool? ImageStatus { get; init; }
    public string? ImageUrl { get; init; }

    // ── Resolution (Editor-only fields) ──
    public string? CorrectiveAction { get; init; }
    public string? ResolutionNotes { get; init; }
    public int? ResolvedBy { get; init; }
    public DateTime? ResolvedOn { get; init; }

    // ── Audit fields ──
    public int UpdatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
    public int CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
    public int? ModifiedBy { get; init; }

    // ── Display helpers (populated by queries, not stored) ──
    public string? ReportedByUsername { get; init; }
    public string? ResolvedByUsername { get; init; }

    public BadFileReport() { }
}
