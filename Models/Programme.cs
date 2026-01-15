using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DV.Shared.Models;

/// <summary>
/// Represents a Programme entity with audit tracking
/// </summary>
[Table("Programme", Schema = "dbo")]
public class Programme
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProgrammeId { get; set; }

    [Required]
    [MaxLength(50)]
    public string ProgrammeName { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public int CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
