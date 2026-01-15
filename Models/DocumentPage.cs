// ============================================================================
// DocumentPage.cs - Model for Document Page Entity
// ============================================================================
//
// Purpose: Represents the "DocumentPage" entity in the application. This model 
// defines the properties and constructors for a document page, including its 
// page number, file details, references, and BLOB storage.
//
// Created: [Date]
// Last Updated: [Date]
//
// Dependencies:
// - System: Provides base types like string and nullable types.
// - Dapper: Uses the parameterless constructor for object mapping.
//
// Notes:
// - This model supports both file-based and BLOB-based storage
// - BLOB storage is preferred for new documents
// - FilePath is maintained for backward compatibility
// ============================================================================

namespace DV.Shared.Models; // Defines the namespace for the model

// ============================================================================
// DocumentPage Record
// ============================================================================
// Purpose: Represents a document page entity with metadata about the page 
// number, file details, references, and binary content storage.
// ============================================================================
public record DocumentPage
{
    // ========================================================================
    // Properties
    // ========================================================================
    // Purpose: Defines the fields for the document page entity.

    // Primary Key
    public int PageId { get; init; } // Unique identifier for the page (NOT NULL)
    
    // Foreign Keys
    public int DocumentId { get; init; } // Identifier for the associated document (NOT NULL)
    
    // Document Index
    public string DocumentIndex { get; init; } = string.Empty; // Document index (NOT NULL)
    
    // Page Information
    public int PageNumber { get; init; } // Page number within the document (NOT NULL)
    public string? PageReference { get; init; } // Optional reference for the page (ALLOW NULL)
    public int? FrameNumber { get; init; } // Frame number (ALLOW NULL)
    
    // Hierarchy Levels
    public string? Level1 { get; init; } // Hierarchy level 1 (ALLOW NULL)
    public string? Level2 { get; init; } // Hierarchy level 2 (ALLOW NULL)
    public string? Level3 { get; init; } // Hierarchy level 3 (ALLOW NULL)
    public string? Level4 { get; init; } // Hierarchy level 4 (ALLOW NULL)
    
    // Disk Information
    public string? DiskNumber { get; init; } // Disk number (ALLOW NULL)
    
    // File Information
    public string FileName { get; init; } = string.Empty; // Name of the file representing the page (NOT NULL)
    public string? FilePath { get; init; } // Path to the file on the server (ALLOW NULL)
    public string FileType { get; init; } = string.Empty; // Type of the file (NOT NULL)
    
    // BLOB storage properties
    public byte[]? FileContent { get; init; } // Binary content of the file (ALLOW NULL)
    public long? FileSize { get; init; } // Size of the file in bytes (ALLOW NULL)
    public int? FileFormat { get; init; } // File format indicator (ALLOW NULL)
    public string? PageSize { get; init; } // Page size (ALLOW NULL)
    public string? ContentType { get; init; } // MIME type of the file (ALLOW NULL)
    public DateTime? UploadedDate { get; init; } // When the file was uploaded to BLOB storage (ALLOW NULL)
    public string? ChecksumMD5 { get; init; } // MD5 checksum for integrity verification (ALLOW NULL)
    
    // Storage type indicator
    public int StorageType { get; init; } // Indicates storage method (NOT NULL) - 0 = FilePath, 1 = Blob
    
    // Audit Fields
    public DateTime CreatedOn { get; init; } // Date and time the page was created (NOT NULL)
    public int CreatedBy { get; init; } // User ID who created the page (NOT NULL)
    public DateTime? ModifiedOn { get; init; } // Date and time the page was last modified (ALLOW NULL)
    public int? ModifiedBy { get; init; } // User ID who last modified the page (ALLOW NULL)

    // ========================================================================
    // Computed Properties
    // ========================================================================
    
    /// <summary>
    /// Indicates whether this document page uses BLOB storage
    /// </summary>
    public bool UsesBlobStorage => StorageType == 1 && FileContent != null;
    
    /// <summary>
    /// Indicates whether this document page uses file path storage
    /// </summary>
    public bool UsesFilePathStorage => StorageType == 0 && !string.IsNullOrEmpty(FilePath);

    // ========================================================================
    // Parameterless Constructor
    // ========================================================================
    // Purpose: Provides a parameterless constructor for Dapper object mapping.
    public DocumentPage() 
    {
        DocumentIndex = string.Empty;
        FileName = string.Empty;
        FileType = string.Empty;
        CreatedOn = DateTime.UtcNow;
    }

    // ========================================================================
    // Comprehensive Constructor
    // ========================================================================
    // Purpose: Initializes a new instance with all available properties
    public DocumentPage(
        int pageId,
        int documentId,
        string documentIndex,
        int pageNumber,
        string? pageReference,
        int? frameNumber,
        string? level1,
        string? level2,
        string? level3,
        string? level4,
        string? diskNumber,
        string fileName,
        string? filePath,
        string fileType,
        byte[]? fileContent,
        long? fileSize,
        int? fileFormat,
        string? pageSize,
        string? contentType,
        DateTime? uploadedDate,
        string? checksumMD5,
        int storageType,
        DateTime createdOn,
        int createdBy,
        DateTime? modifiedOn,
        int? modifiedBy)
    {
        PageId = pageId;
        DocumentId = documentId;
        DocumentIndex = documentIndex;
        PageNumber = pageNumber;
        PageReference = pageReference;
        FrameNumber = frameNumber;
        Level1 = level1;
        Level2 = level2;
        Level3 = level3;
        Level4 = level4;
        DiskNumber = diskNumber;
        FileName = fileName;
        FilePath = filePath;
        FileType = fileType;
        FileContent = fileContent;
        FileSize = fileSize;
        FileFormat = fileFormat;
        PageSize = pageSize;
        ContentType = contentType;
        UploadedDate = uploadedDate;
        ChecksumMD5 = checksumMD5;
        StorageType = storageType;
        CreatedOn = createdOn;
        CreatedBy = createdBy;
        ModifiedOn = modifiedOn;
        ModifiedBy = modifiedBy;
    }
    
    // ========================================================================
    // Simplified Constructor for File-Based Storage
    // ========================================================================
    // Purpose: Initializes a new instance for file-based storage with minimal parameters
    public DocumentPage(int pageId, int documentId, string documentIndex, int pageNumber, 
                       string fileName, string? filePath, string fileType, int createdBy)
    {
        PageId = pageId;
        DocumentId = documentId;
        DocumentIndex = documentIndex;
        PageNumber = pageNumber;
        FileName = fileName;
        FilePath = filePath;
        FileType = fileType;
        StorageType = 0; // FilePath
        CreatedOn = DateTime.UtcNow;
        CreatedBy = createdBy;
    }
    
    // ========================================================================
    // Simplified Constructor for BLOB-Based Storage
    // ========================================================================
    // Purpose: Initializes a new instance for BLOB-based storage with minimal parameters
    public DocumentPage(int pageId, int documentId, string documentIndex, int pageNumber, 
                       string fileName, string fileType, byte[] fileContent, string contentType, 
                       int createdBy, string? checksumMD5 = null)
    {
        PageId = pageId;
        DocumentId = documentId;
        DocumentIndex = documentIndex;
        PageNumber = pageNumber;
        FileName = fileName;
        FileType = fileType;
        FileContent = fileContent;
        FileSize = fileContent?.Length;
        ContentType = contentType;
        UploadedDate = DateTime.UtcNow;
        ChecksumMD5 = checksumMD5;
        StorageType = 1; // Blob
        CreatedOn = DateTime.UtcNow;
        CreatedBy = createdBy;
    }
}

// ============================================================================
// DocumentStorageType Enum
// ============================================================================
// Purpose: Indicates how the document content is stored
public enum DocumentStorageType
{
    FilePath = 0,  // Document is stored as a file on the filesystem
    Blob = 1       // Document is stored as binary data in the database
}
