using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DV.Shared.Security;

/// <summary>
/// Stores local username/password credentials for users who do not use SSO.
/// NIST SP 800-53 Rev 5: IA-5 (Authenticator Management), AC-7 (Unsuccessful Logon Attempts).
/// Passwords are hashed with PBKDF2-SHA512, 600k iterations, per-user 128-bit salt.
/// </summary>
[Table("UserCredential", Schema = "dbo")]
public class UserCredential
{
    [Key]
    public int CredentialId { get; set; }

    /// <summary>FK to ApplicationUser.UserId</summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>PBKDF2-SHA512 hash (Base64-encoded, 64 bytes)</summary>
    [Required, MaxLength(128)]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>Per-user cryptographic salt (Base64-encoded, 16 bytes)</summary>
    [Required, MaxLength(44)]
    public string PasswordSalt { get; set; } = string.Empty;

    /// <summary>PBKDF2 iteration count (stored so it can be upgraded over time)</summary>
    public int Iterations { get; set; } = 600_000;

    /// <summary>Hash algorithm identifier for future migration</summary>
    [Required, MaxLength(20)]
    public string HashAlgorithm { get; set; } = "PBKDF2-SHA512";

    // ── NIST AC-7: Unsuccessful Logon Attempts ──

    /// <summary>Consecutive failed login attempts since last success</summary>
    public int FailedLoginAttempts { get; set; }

    /// <summary>UTC time of last failed login attempt</summary>
    public DateTime? LastFailedLoginAt { get; set; }

    /// <summary>Account is locked due to too many failed attempts</summary>
    public bool IsLockedOut { get; set; }

    /// <summary>UTC time when lockout expires (null = permanent until admin unlock)</summary>
    public DateTime? LockoutEndUtc { get; set; }

    // ── NIST IA-5: Authenticator Lifecycle ──

    /// <summary>When the credential was created</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>When the password was last changed</summary>
    public DateTime? PasswordChangedAt { get; set; }

    /// <summary>UTC time of last successful login</summary>
    public DateTime? LastSuccessfulLoginAt { get; set; }

    /// <summary>Admin who created or last reset this credential</summary>
    [MaxLength(255)]
    public string? CreatedBy { get; set; }

    /// <summary>If true, user must change password at next login</summary>
    public bool MustChangePassword { get; set; } = true;

    // ── Navigation ──
    public virtual ApplicationUser? User { get; set; }
}
