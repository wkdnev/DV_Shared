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
