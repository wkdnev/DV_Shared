namespace DV.Shared.Models;

/// <summary>
/// Metadata for a single file in a bulk upload batch.
/// Maps to the Document table columns in the target schema.
/// </summary>
public class BulkUploadFileMetadata
{
    /// <summary>Client-generated ID to correlate file with metadata</summary>
    public string ClientFileId { get; set; } = string.Empty;

    /// <summary>Original file name as uploaded</summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>File size in bytes</summary>
    public long FileSize { get; set; }

    /// <summary>MIME content type</summary>
    public string? ContentType { get; set; }

    // ── Document-level metadata (mapped to [schema].Document columns) ──

    public string? DocumentIndex { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public DateTime? DocumentDate { get; set; }
    public string? Keywords { get; set; }
    public string? Memo { get; set; }
    public string? Status { get; set; }
    public string? Classification { get; set; }
    public string? Version { get; set; }
    public string? Issue { get; set; }

    // Custom text fields
    public string? Text01 { get; set; }
    public string? Text02 { get; set; }
    public string? Text03 { get; set; }
    public string? Text04 { get; set; }
    public string? Text05 { get; set; }
    public string? Text06 { get; set; }
    public string? Text07 { get; set; }
    public string? Text08 { get; set; }
    public string? Text09 { get; set; }
    public string? Text10 { get; set; }
    public string? Text11 { get; set; }
    public string? Text12 { get; set; }

    // Custom date fields
    public DateTime? Date01 { get; set; }
    public DateTime? Date02 { get; set; }
    public DateTime? Date03 { get; set; }
    public DateTime? Date04 { get; set; }

    // Custom boolean fields
    public bool? Boolean01 { get; set; }
    public bool? Boolean02 { get; set; }
    public bool? Boolean03 { get; set; }

    // Custom number fields
    public double? Number01 { get; set; }
    public double? Number02 { get; set; }
    public double? Number03 { get; set; }

    // ── Validation state (client-side only, not sent to API) ──

    /// <summary>Whether the file passed client-side validation</summary>
    public bool IsValid { get; set; } = true;

    /// <summary>Validation error message if any</summary>
    public string? ValidationError { get; set; }

    /// <summary>Processing status during commit</summary>
    public BulkUploadFileStatus ProcessingStatus { get; set; } = BulkUploadFileStatus.Pending;

    /// <summary>Server-generated DocumentId after successful commit</summary>
    public int? CreatedDocumentId { get; set; }

    /// <summary>Error message from server if processing failed</summary>
    public string? ProcessingError { get; set; }
}

public enum BulkUploadFileStatus
{
    Pending,
    Validating,
    Uploading,
    Success,
    Failed,
    Skipped
}

/// <summary>
/// Request model for the bulk upload API endpoint.
/// Files are sent as multipart form data; metadata is sent as JSON.
/// </summary>
public class BulkUploadRequest
{
    public string SchemaName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public List<BulkUploadFileMetadata> FileMetadata { get; set; } = new();
}

/// <summary>
/// Result for a single file in the bulk upload batch
/// </summary>
public class BulkUploadFileResult
{
    public string ClientFileId { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public bool Success { get; set; }
    public int? DocumentId { get; set; }
    public int? PageId { get; set; }
    public string? Error { get; set; }
}

/// <summary>
/// Overall result of a bulk upload batch
/// </summary>
public class BulkUploadResult
{
    public int TotalFiles { get; set; }
    public int SuccessCount { get; set; }
    public int FailedCount { get; set; }
    public List<BulkUploadFileResult> FileResults { get; set; } = new();
    public TimeSpan Duration { get; set; }
}

/// <summary>
/// CSV template column definitions for the bulk upload CSV format.
/// This defines the canonical CSV format that users can download as a template.
/// </summary>
public static class BulkUploadCsvTemplate
{
    /// <summary>
    /// The required and optional columns in the CSV manifest file.
    /// FileName is the ONLY required column — all others are optional metadata.
    /// </summary>
    public static readonly string[] Columns =
    {
        "FileName",         // REQUIRED — must match an uploaded file
        "DocumentIndex",    // Optional — e.g. INV-2026-001
        "Title",            // Optional — document title (defaults to filename without extension)
        "Author",           // Optional — document author
        "DocumentDate",     // Optional — ISO 8601 date (yyyy-MM-dd)
        "Keywords",         // Optional — semicolon-separated keywords
        "Memo",             // Optional — free text notes
        "Status",           // Optional — e.g. Draft, Final, Approved (defaults to Draft)
        "Classification",   // Optional — e.g. Internal, Confidential, Public
        "Version",          // Optional — e.g. 1.0
        "Issue",            // Optional — issue/revision number
        "Text01", "Text02", "Text03", "Text04", "Text05", "Text06",
        "Text07", "Text08", "Text09", "Text10", "Text11", "Text12",
        "Date01", "Date02", "Date03", "Date04",
        "Boolean01", "Boolean02", "Boolean03",
        "Number01", "Number02", "Number03"
    };

    public static string GenerateHeaderRow() => string.Join(",", Columns);

    public static string GenerateSampleRow() =>
        "example_document.pdf,DOC-001,\"Example Title\",\"John Smith\",2026-01-15,\"keyword1;keyword2\",\"Notes here\",Draft,Internal,1.0,,,,,,,,,,,,,,,,,,,,";
}
