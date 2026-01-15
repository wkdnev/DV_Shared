// ============================================================================
// UserProjectRole.cs - User to Project Role Assignment Entity
// ============================================================================
//
// Purpose: Represents the assignment of a user to a specific project role.
// This is the core entity that enables project-scoped role-based access control.
//
// Created: [Date]
// Last Updated: [Date]
//
// Dependencies:
// - DocViewer_Proto.Security: For ApplicationUser and ProjectRole entities
//
// Notes:
// - This entity replaces the simple UserRole for project-scoped permissions
// - A user can have different roles in different projects
// - This enables separation of duties and need-to-know access control
// ============================================================================

using System.ComponentModel.DataAnnotations;

namespace DV.Shared.Security;

/// <summary>
/// Represents the assignment of a user to a specific project role.
/// This enables users to have different roles in different projects.
/// </summary>
public class UserProjectRole
{
    /// <summary>
    /// The user being assigned the project role
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// The project role being assigned to the user
    /// </summary>
    [Required]
    public int ProjectRoleId { get; set; }

    /// <summary>
    /// When this assignment was created
    /// </summary>
    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Who assigned this role (for audit purposes)
    /// </summary>
    [MaxLength(255)]
    public string? AssignedBy { get; set; }

    /// <summary>
    /// Whether this assignment is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Optional expiration date for the role assignment
    /// </summary>
    public DateTime? ExpiresDate { get; set; }

    /// <summary>
    /// Navigation property to the user
    /// </summary>
    public virtual ApplicationUser? User { get; set; }

    /// <summary>
    /// Navigation property to the project role
    /// </summary>
    public virtual ProjectRole? ProjectRole { get; set; }
}
