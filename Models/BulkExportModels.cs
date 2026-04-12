namespace DV.Shared.Models;

/// <summary>
/// Request to start a bulk export operation.
/// </summary>
public class BulkExportRequest
{
    public string SchemaName { get; set; } = string.Empty;
    public int ProjectId { get; set; }

    /// <summary>All or Filtered</summary>
    public string ExportMode { get; set; } = "All";

    /// <summary>Search term when ExportMode = "Filtered" (legacy simple search)</summary>
    public string? SearchTerm { get; set; }

    /// <summary>Structured filter criteria when ExportMode = "Filtered"</summary>
    public ExportFilterCriteria? Filters { get; set; }

    /// <summary>Zip or Folder</summary>
    public string OutputFormat { get; set; } = "Zip";

    /// <summary>Target folder path when OutputFormat = "Folder"</summary>
    public string? OutputPath { get; set; }

    /// <summary>Whether to include document files (blobs). If false, only CSV metadata is exported.</summary>
    public bool IncludeFiles { get; set; } = true;
}

/// <summary>
/// Structured filter criteria for bulk export.
/// </summary>
public class ExportFilterCriteria
{
    // ── Quick Filters ──
    public List<string> Statuses { get; set; } = new();
    public List<string> Classifications { get; set; } = new();
    public List<string> DocumentTypes { get; set; } = new();

    // ── Text Search ──
    public string? TitleContains { get; set; }
    public string? AuthorContains { get; set; }
    public string? KeywordsContains { get; set; }
    public string? DocumentNumberContains { get; set; }
    public string? MemoContains { get; set; }

    // ── Date Ranges ──
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public DateTime? DocumentDateFrom { get; set; }
    public DateTime? DocumentDateTo { get; set; }

    // ── Custom Field Rules ──
    public List<ExportFieldRule> FieldRules { get; set; } = new();

    /// <summary>Whether all rules must match (AND) or any rule (OR). Default is AND.</summary>
    public string RuleLogic { get; set; } = "AND";

    public bool HasAnyFilter()
    {
        return Statuses.Count > 0
            || Classifications.Count > 0
            || DocumentTypes.Count > 0
            || !string.IsNullOrWhiteSpace(TitleContains)
            || !string.IsNullOrWhiteSpace(AuthorContains)
            || !string.IsNullOrWhiteSpace(KeywordsContains)
            || !string.IsNullOrWhiteSpace(DocumentNumberContains)
            || !string.IsNullOrWhiteSpace(MemoContains)
            || CreatedFrom.HasValue || CreatedTo.HasValue
            || DocumentDateFrom.HasValue || DocumentDateTo.HasValue
            || FieldRules.Count > 0;
    }

    public int ActiveFilterCount()
    {
        int count = 0;
        if (Statuses.Count > 0) count++;
        if (Classifications.Count > 0) count++;
        if (DocumentTypes.Count > 0) count++;
        if (!string.IsNullOrWhiteSpace(TitleContains)) count++;
        if (!string.IsNullOrWhiteSpace(AuthorContains)) count++;
        if (!string.IsNullOrWhiteSpace(KeywordsContains)) count++;
        if (!string.IsNullOrWhiteSpace(DocumentNumberContains)) count++;
        if (!string.IsNullOrWhiteSpace(MemoContains)) count++;
        if (CreatedFrom.HasValue || CreatedTo.HasValue) count++;
        if (DocumentDateFrom.HasValue || DocumentDateTo.HasValue) count++;
        count += FieldRules.Count;
        return count;
    }
}

/// <summary>
/// A single custom field filter rule.
/// </summary>
public class ExportFieldRule
{
    public string Field { get; set; } = string.Empty;
    public string Operator { get; set; } = "contains";
    public string? Value { get; set; }
}

/// <summary>
/// Status tracking for a bulk export job.
/// </summary>
public class BulkExportJobStatus
{
    public Guid JobId { get; set; }
    public string Status { get; set; } = BulkExportStatuses.Queued;
    public string SchemaName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public int TotalDocuments { get; set; }
    public int ExportedDocuments { get; set; }
    public int ExportedFiles { get; set; }
    public int FailedFiles { get; set; }
    public string? CurrentFile { get; set; }
    public string? OutputFormat { get; set; }
    public string? OutputPath { get; set; }
    public long ExportSizeBytes { get; set; }
    public DateTime QueuedAt { get; set; } = DateTime.UtcNow;
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public TimeSpan Elapsed => (CompletedAt ?? DateTime.UtcNow) - (StartedAt ?? QueuedAt);
    public double ProgressPercent => TotalDocuments > 0
        ? Math.Round((double)ExportedDocuments / TotalDocuments * 100, 1)
        : 0;
    public List<string> Errors { get; set; } = new();
    public string? Error { get; set; }
    public int UserId { get; set; }
}

/// <summary>
/// Status constants for bulk export jobs.
/// </summary>
public static class BulkExportStatuses
{
    public const string Queued = "Queued";
    public const string Processing = "Processing";
    public const string Completed = "Completed";
    public const string Failed = "Failed";
    public const string Cancelled = "Cancelled";
}
