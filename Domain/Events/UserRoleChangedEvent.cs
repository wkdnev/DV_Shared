// ============================================================================
// UserRoleChangedEvent.cs - User Role Change Domain Event
// ============================================================================
//
// Purpose: Represents domain events that occur when a user's role assignments
// change within the system. This includes project role assignments, global
// admin status changes, and permission modifications.
//
// Features:
// - Role change tracking with before/after states
// - Project scope identification
// - Administrative action auditing
// - Security event monitoring
//
// Usage:
// - Published by UserService and ProjectRoleService during role changes
// - Handled by audit logging, notification, and security monitoring systems
// - Enables compliance and security tracking
//
// ============================================================================

using DV.Shared.Domain.Events;

namespace DV.Shared.Domain.Events;

/// <summary>
/// Domain event raised when a user's project role assignment changes
/// </summary>
public class UserProjectRoleChangedEvent : DomainEventBase
{
    public UserProjectRoleChangedEvent(
        string username,
        string projectSchema,
        string? previousRole,
        string? newRole,
        string changedByUsername,
        string changeReason,
        string? correlationId = null) : base(correlationId)
    {
        Username = username;
        ProjectSchema = projectSchema;
        PreviousRole = previousRole;
        NewRole = newRole;
        ChangedByUsername = changedByUsername;
        ChangeReason = changeReason;
    }

    /// <summary>
    /// Username of the user whose role changed
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// Project schema where the role change occurred
    /// </summary>
    public string ProjectSchema { get; }

    /// <summary>
    /// Previous role assignment (null if role was added)
    /// </summary>
    public string? PreviousRole { get; }

    /// <summary>
    /// New role assignment (null if role was removed)
    /// </summary>
    public string? NewRole { get; }

    /// <summary>
    /// Username of the administrator who made the change
    /// </summary>
    public string ChangedByUsername { get; }

    /// <summary>
    /// Reason for the role change
    /// </summary>
    public string ChangeReason { get; }

    /// <summary>
    /// Type of change that occurred
    /// </summary>
    public RoleChangeType ChangeType
    {
        get
        {
            if (PreviousRole == null && NewRole != null) return RoleChangeType.Added;
            if (PreviousRole != null && NewRole == null) return RoleChangeType.Removed;
            if (PreviousRole != null && NewRole != null) return RoleChangeType.Modified;
            return RoleChangeType.Unknown;
        }
    }

    public override string ToString()
    {
        return ChangeType switch
        {
            RoleChangeType.Added => $"User '{Username}' assigned role '{NewRole}' in {ProjectSchema} by {ChangedByUsername}",
            RoleChangeType.Removed => $"User '{Username}' removed from role '{PreviousRole}' in {ProjectSchema} by {ChangedByUsername}",
            RoleChangeType.Modified => $"User '{Username}' role changed from '{PreviousRole}' to '{NewRole}' in {ProjectSchema} by {ChangedByUsername}",
            _ => $"User '{Username}' role change in {ProjectSchema} by {ChangedByUsername}"
        };
    }
}

/// <summary>
/// Domain event raised when a user's global admin status changes
/// </summary>
public class UserGlobalAdminStatusChangedEvent : DomainEventBase
{
    public UserGlobalAdminStatusChangedEvent(
        string username,
        bool previousIsGlobalAdmin,
        bool newIsGlobalAdmin,
        string changedByUsername,
        string changeReason,
        string? correlationId = null) : base(correlationId)
    {
        Username = username;
        PreviousIsGlobalAdmin = previousIsGlobalAdmin;
        NewIsGlobalAdmin = newIsGlobalAdmin;
        ChangedByUsername = changedByUsername;
        ChangeReason = changeReason;
    }

    /// <summary>
    /// Username of the user whose admin status changed
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// Previous global admin status
    /// </summary>
    public bool PreviousIsGlobalAdmin { get; }

    /// <summary>
    /// New global admin status
    /// </summary>
    public bool NewIsGlobalAdmin { get; }

    /// <summary>
    /// Username of the administrator who made the change
    /// </summary>
    public string ChangedByUsername { get; }

    /// <summary>
    /// Reason for the status change
    /// </summary>
    public string ChangeReason { get; }

    /// <summary>
    /// Type of admin status change
    /// </summary>
    public AdminChangeType ChangeType
    {
        get
        {
            if (!PreviousIsGlobalAdmin && NewIsGlobalAdmin) return AdminChangeType.Promoted;
            if (PreviousIsGlobalAdmin && !NewIsGlobalAdmin) return AdminChangeType.Demoted;
            return AdminChangeType.NoChange;
        }
    }

    public override string ToString()
    {
        return ChangeType switch
        {
            AdminChangeType.Promoted => $"User '{Username}' promoted to Global Admin by {ChangedByUsername}",
            AdminChangeType.Demoted => $"User '{Username}' removed from Global Admin by {ChangedByUsername}",
            _ => $"User '{Username}' admin status unchanged by {ChangedByUsername}"
        };
    }
}

/// <summary>
/// Types of role changes that can occur
/// </summary>
public enum RoleChangeType
{
    Unknown,
    Added,
    Removed,
    Modified
}

/// <summary>
/// Types of admin status changes
/// </summary>
public enum AdminChangeType
{
    NoChange,
    Promoted,
    Demoted
}

/// <summary>
/// Domain event raised when a user account is created
/// </summary>
public class UserAccountCreatedEvent : DomainEventBase
{
    public UserAccountCreatedEvent(
        string username,
        string? displayName,
        bool isGlobalAdmin,
        string createdByUsername,
        string? correlationId = null) : base(correlationId)
    {
        Username = username;
        DisplayName = displayName;
        IsGlobalAdmin = isGlobalAdmin;
        CreatedByUsername = createdByUsername;
    }

    /// <summary>
    /// Username of the newly created account
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// Display name of the new user
    /// </summary>
    public string? DisplayName { get; }

    /// <summary>
    /// Whether the new user was created as a global admin
    /// </summary>
    public bool IsGlobalAdmin { get; }

    /// <summary>
    /// Username of the administrator who created the account
    /// </summary>
    public string CreatedByUsername { get; }

    public override string ToString()
    {
        var adminStatus = IsGlobalAdmin ? " (Global Admin)" : "";
        return $"User account '{Username}'{adminStatus} created by {CreatedByUsername}";
    }
}