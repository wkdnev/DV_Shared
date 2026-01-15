using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DV.Shared.Security;

/// <summary>
/// Represents an active user session in the application
/// </summary>
[Table("UserSession", Schema = "dbo")]
public class UserSession
{
    [Key]
    public int SessionId { get; set; }

    /// <summary>
    /// ASP.NET Core Session ID
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string SessionKey { get; set; } = string.Empty;

    /// <summary>
    /// User ID associated with this session
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Username associated with this session
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// IP Address of the client
    /// </summary>
    [MaxLength(45)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// User agent string from the browser
    /// </summary>
    [MaxLength(500)]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Current active role for this session
    /// </summary>
    [MaxLength(50)]
    public string? CurrentRole { get; set; }

    /// <summary>
    /// When the session was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Last activity timestamp
    /// </summary>
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When the session expires
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Whether the session is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Whether the session was terminated by an admin
    /// </summary>
    public bool AdminTerminated { get; set; } = false;

    /// <summary>
    /// Username of admin who terminated the session
    /// </summary>
    [MaxLength(255)]
    public string? TerminatedBy { get; set; }

    /// <summary>
    /// When the session was terminated
    /// </summary>
    public DateTime? TerminatedAt { get; set; }

    /// <summary>
    /// Additional metadata as JSON
    /// </summary>
    [MaxLength(1000)]
    public string? Metadata { get; set; }

    // Navigation properties
    public ApplicationUser? User { get; set; }
}

/// <summary>
/// Session activity tracking
/// </summary>
[Table("SessionActivity", Schema = "dbo")]
public class SessionActivity
{
    [Key]
    public int ActivityId { get; set; }

    /// <summary>
    /// Reference to the session
    /// </summary>
    public int SessionId { get; set; }

    /// <summary>
    /// Type of activity (Login, PageView, Search, etc.)
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string ActivityType { get; set; } = string.Empty;

    /// <summary>
    /// Specific action performed
    /// </summary>
    [MaxLength(100)]
    public string? Action { get; set; }

    /// <summary>
    /// Resource accessed (URL, document, etc.)
    /// </summary>
    [MaxLength(500)]
    public string? Resource { get; set; }

    /// <summary>
    /// When the activity occurred
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Additional details about the activity
    /// </summary>
    [MaxLength(1000)]
    public string? Details { get; set; }

    // Navigation properties
    public UserSession Session { get; set; } = null!;
}

/// <summary>
/// Constants for session activity types
/// </summary>
public static class SessionActivityTypes
{
    public const string Login = "Login";
    public const string Logout = "Logout";
    public const string PageView = "PageView";
    public const string Search = "Search";
    public const string DocumentAccess = "DocumentAccess";
    public const string RoleSwitch = "RoleSwitch";
    public const string AdminAction = "AdminAction";
    public const string IdleTimeout = "IdleTimeout";
    public const string AdminTermination = "AdminTermination";
}
