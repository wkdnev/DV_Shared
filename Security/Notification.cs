// ============================================================================
// Notification.cs - In-App Notification Model
// ============================================================================
//
// NIST SP 800-53 Rev 5 Compliance:
//   SI-5:     Security Alerts, Advisories, and Directives
//   SI-5(1):  Automated Alerts and Advisories
//   AU-12:    Audit Record Generation (time-correlated)
//   AU-3:     Content of Audit Records (what, when, where, who, outcome)
//   AC-3:     Access Enforcement (user-scoped notifications)
//   SI-10:    Information Input Validation
//   SI-11:    Error Handling (no exploitable info in messages)
// ============================================================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DV.Shared.Security;

/// <summary>
/// Represents an in-app notification for a specific user.
/// Stored in [dbo].[Notification] and accessed exclusively through the API.
/// </summary>
[Table("Notification", Schema = "dbo")]
public class Notification
{
    [Key]
    public int NotificationId { get; set; }

    /// <summary>
    /// The user this notification is addressed to.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Short notification title (e.g., "Bad File Report Submitted").
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Notification message body. SI-11: Must not contain exploitable information.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Notification category for filtering and routing.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Category { get; set; } = NotificationCategories.General;

    /// <summary>
    /// Whether the user has read this notification.
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Whether this notification is flagged as important/priority.
    /// </summary>
    public bool IsImportant { get; set; }

    /// <summary>
    /// AU-12(1): UTC timestamp for time-correlated audit trail.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When the notification was read (null if unread).
    /// </summary>
    public DateTime? ReadAtUtc { get; set; }

    /// <summary>
    /// Which system component generated this notification.
    /// </summary>
    [MaxLength(50)]
    public string? SourceSystem { get; set; }

    /// <summary>
    /// AU-3: Correlation ID to link notification to the related entity/event.
    /// E.g., "BadFileReport:42" or "BatchImport:7"
    /// </summary>
    [MaxLength(100)]
    public string? CorrelationId { get; set; }

    /// <summary>
    /// Optional expiry for automatic cleanup of old notifications.
    /// </summary>
    public DateTime? ExpiresAtUtc { get; set; }

    // Navigation properties
    public ApplicationUser? User { get; set; }
}

/// <summary>
/// SI-5: Notification categories for routing and filtering.
/// </summary>
public static class NotificationCategories
{
    public const string General = "General";
    public const string BadFileReport = "BadFileReport";
    public const string BatchImport = "BatchImport";
    public const string BatchExport = "BatchExport";
    public const string DocumentUpload = "DocumentUpload";
    public const string Security = "Security";
    public const string System = "System";
    public const string RoleChange = "RoleChange";
    public const string ProjectAccess = "ProjectAccess";
    public const string SessionAlert = "SessionAlert";
}

/// <summary>
/// Source systems that can generate notifications.
/// </summary>
public static class NotificationSources
{
    public const string Api = "API";
    public const string Web = "Web";
    public const string Admin = "Admin";
    public const string System = "System";
    public const string Scheduler = "Scheduler";
}
