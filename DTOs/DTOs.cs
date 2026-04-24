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
    public string? Status { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public string? SortColumn { get; init; }
    public string? SortDirection { get; init; } = "desc";
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

// ============================================================================
// Session Management DTOs
// ============================================================================

public class SessionStatistics
{
    public int TotalActiveSessions { get; set; }
    public int TotalSessionsToday { get; set; }
    public int TotalSessionsYesterday { get; set; }
    public int UniqueUsersToday { get; set; }
    public int AdminTerminatedSessions { get; set; }
    public TimeSpan AverageSessionDuration { get; set; }
    public List<UserSessionSummary> TopActiveUsers { get; set; } = new();
}

public class UserSessionSummary
{
    public int? UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public int SessionCount { get; set; }
    public DateTime LastActivity { get; set; }
}

public record InitializeSessionRequestDto
{
    public string Username { get; init; } = string.Empty;
    public int? UserId { get; init; }
    public string SessionKey { get; init; } = string.Empty;
    public string? IpAddress { get; init; }
    public string? UserAgent { get; init; }
    public string? CurrentRole { get; init; }
}

public record SessionActivityRequestDto
{
    public string SessionKey { get; init; } = string.Empty;
    public string? ActivityType { get; init; }
    public string? Action { get; init; }
    public string? Resource { get; init; }
}

public record UpdateSessionRoleRequestDto
{
    public string SessionKey { get; init; } = string.Empty;
    public string NewRole { get; init; } = string.Empty;
}

public record UpdateSessionRoleByUsernameRequestDto
{
    public string Username { get; init; } = string.Empty;
    public string NewRole { get; init; } = string.Empty;
}

// ============================================================================
// Notification DTOs
// NIST SP 800-53 Rev 5: SI-5, AU-12, AU-3
// ============================================================================

public record NotificationDto
{
    public int NotificationId { get; init; }
    public int UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public bool IsRead { get; init; }
    public bool IsImportant { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public DateTime? ReadAtUtc { get; init; }
    public string? SourceSystem { get; init; }
    public string? CorrelationId { get; init; }
    public string? RelativeTime { get; init; }
}

public record NotificationListRequestDto
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public string? Category { get; init; }
    public bool? IsRead { get; init; }
    public bool? IsImportant { get; init; }
    public string? SortBy { get; init; } = "CreatedAtUtc";
    public bool SortDescending { get; init; } = true;
}

public record NotificationListResponseDto
{
    public List<NotificationDto> Notifications { get; init; } = new();
    public int TotalCount { get; init; }
    public int UnreadCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
}

public record CreateNotificationDto
{
    public int UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public bool IsImportant { get; init; }
    public string? SourceSystem { get; init; }
    public string? CorrelationId { get; init; }
    public int? ExpiresInDays { get; init; }
}

public record BulkNotificationActionDto
{
    public List<int> NotificationIds { get; init; } = new();
}

public record UnreadCountDto
{
    public int UnreadCount { get; init; }
    public int ImportantUnreadCount { get; init; }
}

public record MarkAllReadResponseDto
{
    public int MarkedCount { get; init; }
}

public record BulkDeleteResponseDto
{
    public int DeletedCount { get; init; }
}

// ============================================================================
// Bad File Report DTOs
// ============================================================================

public record BadFileReportDto
{
    public int BadFileReportId { get; init; }
    public int ReportedBy { get; init; }
    public string? ReportedByUsername { get; init; }
    public DateTime ReportedOn { get; init; }
    public int DocumentPageId { get; init; }
    public int DocumentId { get; init; }
    public string SchemaName { get; init; } = string.Empty;
    public string? FileName { get; init; }
    public int PageNumber { get; init; }
    public string ReportType { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Priority { get; init; } = "Normal";
    public string Status { get; init; } = "Open";
    public string? CorrectiveAction { get; init; }
    public string? ResolutionNotes { get; init; }
    public int? ResolvedBy { get; init; }
    public string? ResolvedByUsername { get; init; }
    public DateTime? ResolvedOn { get; init; }
    public DateTime CreatedOn { get; init; }
}

public record CreateBadFileReportDto
{
    public int DocumentPageId { get; init; }
    public int DocumentId { get; init; }
    public string SchemaName { get; init; } = string.Empty;
    public string? FileName { get; init; }
    public int PageNumber { get; init; }
    public string ReportType { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Priority { get; init; } = "Normal";
    public string? ImageUrl { get; init; }
}

public record UpdateBadFileReportDto
{
    public string? Status { get; init; }
    public string? CorrectiveAction { get; init; }
    public string? ResolutionNotes { get; init; }
}

public record BadFileReportListRequestDto
{
    public string SchemaName { get; init; } = string.Empty;
    public string? Status { get; init; }
    public string? ReportType { get; init; }
    public string? Priority { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

public record BadFileReportListResponseDto
{
    public List<BadFileReportDto> Reports { get; init; } = new();
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public int OpenCount { get; init; }
    public int InProgressCount { get; init; }
    public int ResolvedCount { get; init; }
}
