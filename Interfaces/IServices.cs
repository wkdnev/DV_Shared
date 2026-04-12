// ============================================================================
// Service Interfaces - Contracts for Services
// ============================================================================

using DV.Shared.DTOs;
using DV.Shared.Models;
using DV.Shared.Security;

namespace DV.Shared.Interfaces;

// ============================================================================
// Document Service
// ============================================================================

public interface IDocumentService
{
    Task<DocumentSearchResponseDto> SearchDocumentsAsync(DocumentSearchRequestDto request);
    Task<DocumentDto?> GetDocumentByIdAsync(int documentId);
    Task<List<DocumentDto>> GetDocumentsByProjectAsync(int projectId);
    Task<DocumentDto> CreateDocumentAsync(DocumentDto document);
    Task<DocumentDto> UpdateDocumentAsync(DocumentDto document);
    Task<bool> DeleteDocumentAsync(int documentId, string deletedBy);
}

// ============================================================================
// Project Service
// ============================================================================

public interface IProjectService
{
    Task<List<ProjectDto>> GetAllProjectsAsync();
    Task<ProjectDto?> GetProjectByIdAsync(int projectId);
    Task<List<ProjectDto>> GetProjectsForUserAsync(int userId);
    Task<ProjectDto> CreateProjectAsync(ProjectDto project);
    Task<ProjectDto> UpdateProjectAsync(ProjectDto project);
}

// ============================================================================
// User Service
// ============================================================================

public interface IUserService
{
    Task<ApplicationUser?> GetUserByUsernameAsync(string username);
    Task<ApplicationUser?> GetUserByIdAsync(int userId);
    Task<ApplicationUser> EnsureUserExistsAsync(string username, string displayName, string email);
    Task<List<RoleDto>> GetUserRolesAsync(int userId);
    Task<bool> IsGlobalAdminAsync(int userId);
}

// ============================================================================
// Role Context Service
// ============================================================================

public interface IRoleContextService
{
    Task InitializeAsync();
    bool IsInitialized { get; }
    List<string> AvailableRoles { get; }
    string? CurrentRole { get; }
    bool HasMultipleRoles { get; }
    bool IsGlobalAdmin { get; }
    Task<bool> SetActiveRoleAsync(string roleName);
}

// ============================================================================
// Authentication Service
// ============================================================================

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    Task<LoginResponseDto> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync();
    Task<UserInfoDto?> GetCurrentUserAsync();
    bool IsAuthenticated { get; }
}

// ============================================================================
// Session Management Service (NIST SP 800-53 Rev 5 Compliant)
// ============================================================================

public interface ISessionManagementService
{
    Task<UserSession> InitializeSessionAsync(string username, int? userId, string? currentRole = null);
    Task UpdateSessionActivityAsync(string? activityType = null, string? action = null, string? resource = null);
    Task<bool> IsSessionValidAsync(string? sessionKey);
    Task UpdateSessionRoleAsync(string newRole);
    Task UpdateSessionRoleByUsernameAsync(string username, string newRole);
    Task<bool> TerminateSessionAsync(string sessionKey, string? terminatedBy = null, bool isAdminTermination = false);
    Task TerminateAllUserSessionsAsync(string username, string? terminatedBy = null);
    Task<List<UserSession>> GetActiveSessionsAsync();
    Task<UserSession?> GetSessionByIdAsync(int sessionId);
    Task<List<UserSession>> GetUserSessionsAsync(int userId, bool activeOnly = true);
    Task<SessionStatistics> GetSessionStatisticsAsync();
    Task<int> CleanupExpiredSessionsAsync();
}

// ============================================================================
// Credential Service (NIST SP 800-53 Rev 5: IA-5, AC-7)
// ============================================================================

/// <summary>
/// Manages local username/password credentials.
/// NIST IA-5: Authenticator management (hashing, salt, iteration count).
/// NIST AC-7: Account lockout after failed attempts.
/// </summary>
public interface ICredentialService
{
    /// <summary>Create credential for a user (admin-initiated). Returns validation errors or null on success.</summary>
    Task<string?> CreateCredentialAsync(int userId, string password, string createdBy);

    /// <summary>Validate username+password. Returns the ApplicationUser on success, null on failure. Handles lockout.</summary>
    Task<ApplicationUser?> ValidateCredentialAsync(string username, string password);

    /// <summary>Change password (user-initiated, requires old password).</summary>
    Task<string?> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

    /// <summary>Reset password (admin-initiated, no old password required).</summary>
    Task<string?> AdminResetPasswordAsync(int userId, string newPassword, string adminUsername);

    /// <summary>Unlock a locked account.</summary>
    Task UnlockAccountAsync(int userId);

    /// <summary>Check if a user has local credentials set up.</summary>
    Task<bool> HasCredentialAsync(int userId);

    /// <summary>Remove local credentials for a user.</summary>
    Task RemoveCredentialAsync(int userId);

    /// <summary>Get credential metadata (no hash/salt) for admin display.</summary>
    Task<UserCredential?> GetCredentialInfoAsync(int userId);

    /// <summary>Validate password complexity (NIST SP 800-63B). Returns error message or null.</summary>
    string? ValidatePasswordComplexity(string password);
}

// ============================================================================
// Notification Service (NIST SP 800-53 Rev 5: SI-5, AU-12)
// ============================================================================

public interface INotificationService
{
    Task<NotificationListResponseDto> GetNotificationsAsync(int userId, NotificationListRequestDto request);
    Task<UnreadCountDto> GetUnreadCountAsync(int userId);
    Task<NotificationDto?> GetNotificationByIdAsync(int notificationId, int userId);
    Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto request);
    Task<bool> MarkAsReadAsync(int notificationId, int userId);
    Task<int> MarkAllAsReadAsync(int userId);
    Task<bool> DeleteNotificationAsync(int notificationId, int userId);
    Task<int> BulkDeleteAsync(List<int> notificationIds, int userId);
    Task<int> CleanupExpiredNotificationsAsync();
}

// ============================================================================
// Access Group Service (Application-Managed Groups)
// ============================================================================

/// <summary>
/// Manages application-managed access groups that work alongside AD groups
/// for project access control via ReadPrincipal / EditPrincipal.
/// </summary>
public interface IAccessGroupService
{
    // ── Group CRUD ──
    Task<List<AccessGroup>> GetAllGroupsAsync();
    Task<AccessGroup?> GetGroupByIdAsync(int groupId);
    Task<AccessGroup?> GetGroupByNameAsync(string groupName);
    Task<AccessGroup> CreateGroupAsync(string groupName, string? description, string createdBy);
    Task UpdateGroupAsync(int groupId, string groupName, string? description);
    Task DeleteGroupAsync(int groupId);
    Task SetGroupActiveAsync(int groupId, bool isActive);

    // ── Membership ──
    Task<List<AccessGroupMember>> GetGroupMembersAsync(int groupId);
    Task AddMemberAsync(int groupId, int userId, string addedBy);
    Task RemoveMemberAsync(int groupId, int userId);
    Task<bool> IsMemberAsync(int groupId, int userId);

    // ── Access resolution (used by DocumentRepository) ──
    /// <summary>
    /// Returns all active group names that a user belongs to.
    /// Used alongside AD claims to resolve project access.
    /// </summary>
    Task<HashSet<string>> GetUserGroupNamesAsync(int userId);

    /// <summary>
    /// Checks if a user is a member of an active group with the given name.
    /// </summary>
    Task<bool> IsUserInGroupAsync(int userId, string groupName);
}

// ============================================================================
// Bad File Report Service
// ============================================================================

public interface IBadFileReportService
{
    Task<BadFileReportListResponseDto> GetReportsAsync(BadFileReportListRequestDto request);
    Task<BadFileReportDto?> GetReportByIdAsync(string schemaName, int reportId);
    Task<BadFileReportDto> CreateReportAsync(CreateBadFileReportDto request, int userId);
    Task<BadFileReportDto?> UpdateReportAsync(string schemaName, int reportId, UpdateBadFileReportDto request, int userId);
}
