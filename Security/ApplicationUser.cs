using System;

namespace DV.Shared.Security;

public class ApplicationUser
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }
    
    /// <summary>
    /// Indicates if this user is a global system administrator.
    /// Global admins have unrestricted access to all system functions and projects.
    /// </summary>
    public bool? IsGlobalAdmin { get; set; } = false;
}
