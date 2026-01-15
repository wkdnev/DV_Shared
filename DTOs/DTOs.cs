// ============================================================================
// DTOs - Data Transfer Objects for API Communication
// ============================================================================

namespace DV.Shared.DTOs;

// ============================================================================
// Authentication DTOs
// ============================================================================

public record LoginRequestDto
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public record LoginResponseDto
{
    public bool Success { get; init; }
    public string? AccessToken { get; init; }
    public string? RefreshToken { get; init; }
    public DateTime? ExpiresAt { get; init; }
    public UserInfoDto? User { get; init; }
    public string? Error { get; init; }
}

public record UserInfoDto
{
    public int UserId { get; init; }
    public string Username { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public bool IsGlobalAdmin { get; init; }
    public List<string> Roles { get; init; } = new();
    public string? CurrentRole { get; init; }
}

// ============================================================================
// Document DTOs
// ============================================================================

public record DocumentDto
{
    public int DocumentId { get; init; }
    public int? ProjectId { get; init; }
    public string DocumentNumber { get; init; } = string.Empty;
    public string? Title { get; init; }
    public string? Author { get; init; }
    public DateTime? DocumentDate { get; init; }
    public string? DocumentType { get; init; }
    public string? Status { get; init; }
    public string? Classification { get; init; }
    public int PageCount { get; init; }
}

public record DocumentSearchRequestDto
{
    public string? SearchTerm { get; init; }
    public int? ProjectId { get; init; }
    public string? DocumentType { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

public record DocumentSearchResponseDto
{
    public List<DocumentDto> Documents { get; init; } = new();
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
}

// ============================================================================
// Project DTOs
// ============================================================================

public record ProjectDto
{
    public int ProjectId { get; init; }
    public string ProjectCode { get; init; } = string.Empty;
    public string ProjectName { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    public int DocumentCount { get; init; }
}

// ============================================================================
// Role DTOs
// ============================================================================

public record RoleDto
{
    public int ProjectRoleId { get; init; }
    public string RoleName { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsSystemRole { get; init; }
}

public record SetActiveRoleRequestDto
{
    public string RoleName { get; init; } = string.Empty;
}

public record SetActiveRoleResponseDto
{
    public bool Success { get; init; }
    public string? CurrentRole { get; init; }
    public string? RedirectUrl { get; init; }
    public string? Error { get; init; }
}

// ============================================================================
// Generic API Response
// ============================================================================

public record ApiResponse<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? Error { get; init; }
    public List<string> Errors { get; init; } = new();
}
