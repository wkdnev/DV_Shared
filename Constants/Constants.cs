// ============================================================================
// Constants and Enums
// ============================================================================

namespace DV.Shared.Constants;

/// <summary>
/// Application-wide role constants
/// </summary>
public static class Roles
{
    public const string Admin = "Admin";
    public const string ReadOnly = "ReadOnly";
    public const string Auditor = "Auditor";
    public const string Editor = "Editor";
    public const string GlobalAdmin = "GlobalAdmin";
    public const string GlobalAdminGroup = "DocViewer_GlobalAdmins";
    public const string AdminGroup = "DocViewer_Admins";
    public const string AuditorGroup = "DocViewer_Auditors";
    public const string SecurityGroup = "DocViewer_Security";
    public const string Security = "Security";
}

/// <summary>
/// API route constants
/// </summary>
public static class ApiRoutes
{
    public const string Base = "/api";
    
    public static class Auth
    {
        public const string Login = $"{Base}/auth/login";
        public const string Logout = $"{Base}/auth/logout";
        public const string Refresh = $"{Base}/auth/refresh";
        public const string UserInfo = $"{Base}/auth/userinfo";
    }
    
    public static class Documents
    {
        public const string Base = $"{ApiRoutes.Base}/documents";
        public const string Search = $"{Base}/search";
        public const string ById = $"{Base}/{{id}}";
        public const string ByProject = $"{Base}/project/{{projectId}}";
    }
    
    public static class Projects
    {
        public const string Base = $"{ApiRoutes.Base}/projects";
        public const string ById = $"{Base}/{{id}}";
        public const string ForUser = $"{Base}/user/{{userId}}";
    }
    
    public static class Users
    {
        public const string Base = $"{ApiRoutes.Base}/users";
        public const string ById = $"{Base}/{{id}}";
        public const string Roles = $"{Base}/{{id}}/roles";
        public const string SetRole = $"{Base}/set-role";
    }

    public static class Notifications
    {
        public const string Base = $"{ApiRoutes.Base}/notifications";
        public const string UnreadCount = $"{Base}/unread-count";
        public const string ById = $"{Base}/{{id}}";
        public const string MarkRead = $"{Base}/{{id}}/read";
        public const string MarkAllRead = $"{Base}/read-all";
        public const string BulkDelete = $"{Base}/bulk";
        public const string Cleanup = $"{Base}/cleanup";
    }
}

/// <summary>
/// Claim types used in authentication
/// </summary>
public static class ClaimTypes
{
    public const string UserId = "user_id";
    public const string Username = "unique_name";
    public const string DisplayName = "display_name";
    public const string Email = "email";
    public const string IsGlobalAdmin = "is_global_admin";
    public const string CurrentRole = "current_role";
    public const string Roles = "roles";
}

/// <summary>
/// Session management configuration constants
/// NIST SP 800-53 Rev 5 compliant (AC-10, AC-11, AC-12, SC-23)
/// </summary>
public static class SessionConfig
{
    /// <summary>AC-12: Idle timeout in minutes (sliding). Sessions with no activity expire.</summary>
    public const int IdleTimeoutMinutes = 30;

    /// <summary>AC-12: Absolute timeout in hours. Sessions cannot exceed this regardless of activity.</summary>
    public const int AbsoluteTimeoutHours = 8;

    /// <summary>AC-10: Maximum concurrent sessions per user.</summary>
    public const int MaxConcurrentSessionsPerUser = 3;

    /// <summary>Background cleanup interval in minutes.</summary>
    public const int CleanupIntervalMinutes = 5;

    /// <summary>Throttle session activity DB writes (seconds between writes per session).</summary>
    public const int ActivityThrottleSeconds = 60;

    /// <summary>SC-23: Custom cookie name to prevent technology fingerprinting.</summary>
    public const string CookieName = "DV.SessionId";

    /// <summary>AC-12(03): Minutes before idle timeout to show warning to user.</summary>
    public const int TimeoutWarningMinutes = 5;

    /// <summary>AC-11: Minutes of inactivity before the session lock screen activates.</summary>
    public const int SessionLockMinutes = 25;
}
