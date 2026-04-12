using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DV.Shared.Models;

public record Document
{
    [NotMapped]
    public string? SchemaName { get; set; }

    [Key]
    public int DocumentId { get; init; }
    public int? ProjectId { get; init; }
    public string? DocumentIndex { get; init; }
    public string? Issue { get; init; }
    public string? DocumentStatus { get; init; }
    public string DocumentNumber { get; init; } = string.Empty;
    public string? Title { get; init; }
    public string? Author { get; init; }
    public DateTime? DocumentDate { get; init; }
    public string? Keywords { get; init; }
    public string? Memo { get; init; }
    public string? DocumentType { get; init; }
    public int? OldDM { get; init; }
    public int? CM { get; init; }
    public int? GM { get; init; }
    public int? EM { get; init; }
    public string? Text01 { get; init; }
    public string? Text02 { get; init; }
    public string? Text03 { get; init; }
    public string? Text04 { get; init; }
    public string? Text05 { get; init; }
    public string? Text06 { get; init; }
    public string? Text07 { get; init; }
    public string? Text08 { get; init; }
    public string? Text09 { get; init; }
    public string? Text10 { get; init; }
    public string? Text11 { get; init; }
    public string? Text12 { get; init; }
    public DateTime? Date01 { get; init; }
    public DateTime? Date02 { get; init; }
    public DateTime? Date03 { get; init; }
    public DateTime? Date04 { get; init; }
    public bool? Boolean01 { get; init; }
    public bool? Boolean02 { get; init; }
    public bool? Boolean03 { get; init; }
    public double? Number01 { get; init; }
    public double? Number02 { get; init; }
    public double? Number03 { get; init; }
    public string? Version { get; init; }
    public string? Status { get; init; }
    public string? Classification { get; init; }
    public string? FilePath { get; init; }
    public DateTime CreatedOn { get; init; }
    public int CreatedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }
    public int? ModifiedBy { get; init; }
    public string? PublicToken { get; init; }

    public Document() { }
}
