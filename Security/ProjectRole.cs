// ============================================================================
// ProjectRole.cs - Project-Specific Role Entity
// ============================================================================
//
// Purpose: Represents a role that is scoped to a specific project. This allows
// for different role implementations per project (e.g., "Invoices-Admin" vs 
// "Correspondence-Admin") with potentially different permissions.
//
// Created: [Date]
// Last Updated: [Date]
//
// Dependencies:
// - DocViewer_Proto.Security: For ApplicationRole and security context
// - DocViewer_Proto.Models: For Project entity
//
// Notes:
// - This entity bridges Projects and Roles for project-scoped authorization
// - A ProjectRole represents a specific role within a specific project context
// - Different projects can have the same role name with different permissions
// ============================================================================

using System.ComponentModel.DataAnnotations;

namespace DV.Shared.Security;

/// <summary>
/// Represents a role that is scoped to a specific project.
/// This allows different projects to have their own role definitions and permissions.
/// </summary>
public class ProjectRole
{
    /// <summary>
    /// Unique identifier for the project role
    /// </summary>
    [Key]
    public int ProjectRoleId { get; set; }

    /// <summary>
    /// The project this role belongs to
    /// </summary>
    [Required]
    public int ProjectId { get; set; }

    /// <summary>
    /// The base role template this project role is based on (references ApplicationRole catalog)
    /// </summary>
    [Required]
    public int ApplicationRoleId { get; set; }

    /// <summary>
    /// Display name for this project role (e.g., "Invoices Admin", "Correspondence Editor")
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Optional description specific to this project role
    /// </summary>
    [MaxLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Whether this project role is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// When this project role was created
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Navigation property to the base role template
    /// </summary>
    public virtual ApplicationRole? ApplicationRole { get; set; }

    /// <summary>
    /// Navigation property to user assignments for this project role
    /// </summary>
    public virtual ICollection<UserProjectRole> UserProjectRoles { get; set; } = new List<UserProjectRole>();

    // REMOVED: ProjectRolePermissions navigation - permissions system has been removed
    // Authorization is now based on project roles only, without granular permission assignments
}
