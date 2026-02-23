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
