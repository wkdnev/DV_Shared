using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DV.Shared.Security;

/// <summary>
/// Application-managed access group — a virtual equivalent of an AD security group.
/// Can be assigned to Project.ReadPrincipal / EditPrincipal alongside real AD groups.
/// </summary>
[Table("AccessGroup", Schema = "dbo")]
public class AccessGroup
{
    [Key]
    public int AccessGroupId { get; set; }

    [Required, MaxLength(255)]
    public string GroupName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [MaxLength(255)]
    public string? CreatedBy { get; set; }

    // Navigation
    public virtual ICollection<AccessGroupMember> Members { get; set; } = new List<AccessGroupMember>();
}
