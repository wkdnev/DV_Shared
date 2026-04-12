using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DV.Shared.Security;

/// <summary>
/// Maps a user to an application-managed access group.
/// When a project's ReadPrincipal or EditPrincipal matches the group's GroupName,
/// members of that group gain access to the project.
/// </summary>
[Table("AccessGroupMember", Schema = "dbo")]
public class AccessGroupMember
{
    [Key]
    public int AccessGroupMemberId { get; set; }

    [Required]
    public int AccessGroupId { get; set; }

    [Required]
    public int UserId { get; set; }

    public DateTime AddedDate { get; set; } = DateTime.UtcNow;

    [MaxLength(255)]
    public string? AddedBy { get; set; }

    // Navigation
    public virtual AccessGroup? AccessGroup { get; set; }
    public virtual ApplicationUser? User { get; set; }
}
