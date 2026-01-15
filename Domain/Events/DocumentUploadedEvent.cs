// ============================================================================
// DocumentUploadedEvent.cs - Document Upload Domain Event
// ============================================================================
//
// Purpose: Represents the domain event that occurs when a document is
// successfully uploaded to the system. This event can trigger various
// side effects such as indexing, notifications, audit logging, etc.
//
// Features:
// - Document metadata capture
// - User context tracking
// - Project scope identification
// - File processing status
//
// Usage:
// - Published by DocumentUploadService after successful upload
// - Handled by various event handlers for post-upload processing
// - Enables decoupled document processing workflows
//
// ============================================================================

using DV.Shared.Domain.Events;

namespace DV.Shared.Domain.Events;

/// <summary>
/// Domain event raised when a document is successfully uploaded to the system
/// </summary>
public class DocumentUploadedEvent : DomainEventBase
{
    public DocumentUploadedEvent(
        int documentId,
        string fileName,
        string projectSchema,
        string uploadedByUsername,
        long fileSizeBytes,
        string contentType,
        int pageCount = 0,
        string? correlationId = null) : base(correlationId)
    {
        DocumentId = documentId;
        FileName = fileName;
        ProjectSchema = projectSchema;
        UploadedByUsername = uploadedByUsername;
        FileSizeBytes = fileSizeBytes;
        ContentType = contentType;
        PageCount = pageCount;
    }

    /// <summary>
    /// The unique identifier of the uploaded document
    /// </summary>
    public int DocumentId { get; }

    /// <summary>
    /// Original name of the uploaded file
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// Project schema where the document was uploaded
    /// </summary>
    public string ProjectSchema { get; }

    /// <summary>
    /// Username of the person who uploaded the document
    /// </summary>
    public string UploadedByUsername { get; }

    /// <summary>
    /// Size of the uploaded file in bytes
    /// </summary>
    public long FileSizeBytes { get; }

    /// <summary>
    /// MIME type of the uploaded file
    /// </summary>
    public string ContentType { get; }

    /// <summary>
    /// Number of pages processed (for PDF documents)
    /// </summary>
    public int PageCount { get; }

    /// <summary>
    /// Whether the document upload completed successfully
    /// </summary>
    public bool IsSuccessful => DocumentId > 0;

    /// <summary>
    /// Formatted file size for display
    /// </summary>
    public string FormattedFileSize
    {
        get
        {
            if (FileSizeBytes < 1024) return $"{FileSizeBytes} B";
            if (FileSizeBytes < 1024 * 1024) return $"{FileSizeBytes / 1024.0:F1} KB";
            if (FileSizeBytes < 1024 * 1024 * 1024) return $"{FileSizeBytes / (1024.0 * 1024.0):F1} MB";
            return $"{FileSizeBytes / (1024.0 * 1024.0 * 1024.0):F1} GB";
        }
    }

    public override string ToString()
    {
        return $"Document '{FileName}' uploaded to {ProjectSchema} by {UploadedByUsername} " +
               $"({FormattedFileSize}, {PageCount} pages)";
    }
}

/// <summary>
/// Domain event raised when document upload fails
/// </summary>
public class DocumentUploadFailedEvent : DomainEventBase
{
    public DocumentUploadFailedEvent(
        string fileName,
        string projectSchema,
        string uploadedByUsername,
        string errorMessage,
        string? errorCode = null,
        string? correlationId = null) : base(correlationId)
    {
        FileName = fileName;
        ProjectSchema = projectSchema;
        UploadedByUsername = uploadedByUsername;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Name of the file that failed to upload
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// Project schema where upload was attempted
    /// </summary>
    public string ProjectSchema { get; }

    /// <summary>
    /// Username of the person who attempted the upload
    /// </summary>
    public string UploadedByUsername { get; }

    /// <summary>
    /// Description of the error that occurred
    /// </summary>
    public string ErrorMessage { get; }

    /// <summary>
    /// Optional error code for categorization
    /// </summary>
    public string? ErrorCode { get; }

    public override string ToString()
    {
        return $"Document upload failed: '{FileName}' to {ProjectSchema} by {UploadedByUsername} - {ErrorMessage}";
    }
}