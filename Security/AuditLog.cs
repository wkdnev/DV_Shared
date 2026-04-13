using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DV.Shared.Security;

/// <summary>
/// Represents an audit log entry for tracking user activities and security events
/// </summary>
[Table("AuditLog", Schema = "dbo")]
public class AuditLog
{
    [Key]
    public int AuditLogId { get; set; }

    /// <summary>
    /// Type of event being audited
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string EventType { get; set; } = string.Empty;

    /// <summary>
    /// Specific action that was performed
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Username of the user who performed the action
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// User ID (if available)
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Project ID related to the action (if applicable)
    /// </summary>
    public int? ProjectId { get; set; }

    /// <summary>
    /// Document ID related to the action (if applicable)
    /// </summary>
    public int? DocumentId { get; set; }

    /// <summary>
    /// Resource identifier (e.g., role name, permission name, etc.)
    /// </summary>
    [MaxLength(255)]
    public string? ResourceId { get; set; }

    /// <summary>
    /// Result of the action (Success, Failed, Denied, etc.)
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Result { get; set; } = string.Empty;

    /// <summary>
    /// Additional details about the action
    /// </summary>
    [MaxLength(1000)]
    public string? Details { get; set; }

    /// <summary>
    /// IP Address of the user
    /// </summary>
    [MaxLength(45)] // IPv6 max length
    public string? IpAddress { get; set; }

    /// <summary>
    /// User agent string
    /// </summary>
    [MaxLength(500)]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Session ID
    /// </summary>
    [MaxLength(100)]
    public string? SessionId { get; set; }

    /// <summary>
    /// Timestamp when the event occurred
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Duration of the operation in milliseconds (if applicable)
    /// </summary>
    public long? DurationMs { get; set; }

    /// <summary>
    /// Additional metadata as JSON
    /// </summary>
    [MaxLength(2000)]
    public string? Metadata { get; set; }

    /// <summary>
    /// SHA-256 hash of the previous audit log record's RecordHash.
    /// Forms a cryptographic chain per NIST SP 800-53 AU-9(3).
    /// Genesis value: 64 zeros.
    /// </summary>
    [MaxLength(64)]
    public string? PreviousHash { get; set; }

    /// <summary>
    /// SHA-256 hash of this record's canonical fields + PreviousHash.
    /// Used for tamper detection per NIST SP 800-53 AU-9(3).
    /// </summary>
    [MaxLength(64)]
    public string? RecordHash { get; set; }

    // Navigation properties
    public ApplicationUser? User { get; set; }

    /// <summary>
    /// Genesis hash used as PreviousHash for the first record in the chain
    /// </summary>
    public const string GenesisHash = "0000000000000000000000000000000000000000000000000000000000000000";
}

/// <summary>
/// Enumeration of audit event types
/// </summary>
public static class AuditEventTypes
{
    public const string Authentication = "Authentication";
    public const string Authorization = "Authorization";
    public const string DocumentAccess = "DocumentAccess";
    public const string ProjectAccess = "ProjectAccess";
    public const string RoleManagement = "RoleManagement";
    public const string UserManagement = "UserManagement";
    public const string SystemAdmin = "SystemAdmin";
    public const string DataModification = "DataModification";
    public const string SecurityEvent = "SecurityEvent";
}

/// <summary>
/// Enumeration of audit actions
/// </summary>
public static class AuditActions
{
    // Authentication actions
    public const string Login = "Login";
    public const string Logout = "Logout";
    public const string LoginFailed = "LoginFailed";

    // Document access actions
    public const string ViewDocument = "ViewDocument";
    public const string DownloadDocument = "DownloadDocument";
    public const string SearchDocuments = "SearchDocuments";
    public const string ViewDocumentMetadata = "ViewDocumentMetadata";

    // Project access actions
    public const string AccessProject = "AccessProject";
    public const string ProjectAccessDenied = "ProjectAccessDenied";
    public const string GrantProjectAccess = "GrantProjectAccess";
    public const string RevokeProjectAccess = "RevokeProjectAccess";

    // Role management actions
    public const string AssignRole = "AssignRole";
    public const string RemoveRole = "RemoveRole";
    public const string CreateRole = "CreateRole";
    public const string DeleteRole = "DeleteRole";
    public const string ModifyRole = "ModifyRole";

    // User management actions
    public const string CreateUser = "CreateUser";
    public const string ModifyUser = "ModifyUser";
    public const string DeleteUser = "DeleteUser";
    public const string EnableUser = "EnableUser";
    public const string DisableUser = "DisableUser";

    // Admin override actions
    public const string AdminOverride = "AdminOverride";
    public const string GlobalAdminAccess = "GlobalAdminAccess";

    // Security events
    public const string UnauthorizedAccess = "UnauthorizedAccess";
    public const string SuspiciousActivity = "SuspiciousActivity";
    public const string SecurityViolation = "SecurityViolation";
}

/// <summary>
/// Enumeration of audit results
/// </summary>
public static class AuditResults
{
    public const string Success = "Success";
    public const string Failed = "Failed";
    public const string Denied = "Denied";
    public const string Error = "Error";
    public const string Warning = "Warning";
}
