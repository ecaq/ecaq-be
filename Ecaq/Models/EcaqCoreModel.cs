using System.ComponentModel.DataAnnotations;

namespace Ecaq.Models;

public class EcaqCoreModel : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Desc { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string? ThumbUrl { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty; // to do remove nullable

}
