using System.ComponentModel.DataAnnotations;

namespace Ecaq.Models;

public class AboutModel : BaseEntity
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;


    [DataType(DataType.MultilineText)]
    public string Desc { get; set; } = string.Empty;
    [DataType(DataType.MultilineText)]
    public string Desc2 { get; set; } = string.Empty;


    public string StatNationalityText { get; set; } = string.Empty;
    public string StatNationalityValue { get; set; } = string.Empty;

    public string StatMembersText { get; set; } = string.Empty;
    public string StatMembersyValue { get; set; } = string.Empty; // to do

    public string StatChurchesText { get; set; } = string.Empty;
    public string StatChurchesValue { get; set; } = string.Empty;

    public string StatAllianceText { get; set; } = string.Empty;
    public string StatAlianceValue { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public string BackgroundImage { get; set; }
}
