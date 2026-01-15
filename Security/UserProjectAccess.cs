using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DV.Shared.Security;

/// <summary>
/// Represents a user's authorization to access a specific project.
/// Users must have project access before they can be assigned project roles.
/// </summary>
[Table("UserProjectAccess", Schema = "dbo")]
public class UserProjectAccess
{
    [Key, Column(Order = 1)]
    public int UserId { get; set; }

    [Key, Column(Order = 2)]
    public int ProjectId { get; set; }

    /// <summary>
    /// Date when access was granted
    /// </summary>
    public DateTime GrantedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Who granted this access
    /// </summary>
    [MaxLength(255)]
    public string? GrantedBy { get; set; }

    /// <summary>
    /// Whether this access is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Optional expiration date for temporary access
    /// </summary>
    public DateTime? ExpiresDate { get; set; }

    /// <summary>
    /// Reason or notes for granting access
    /// </summary>
    [MaxLength(500)]
    public string? AccessReason { get; set; }

    // Navigation properties
    public ApplicationUser? User { get; set; }
    
    // Note: We don't include Project navigation property to avoid circular dependencies
    // Project information can be retrieved via ProjectService when needed
}
