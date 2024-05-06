namespace Ecaq.Models;

public class MemberModel : BaseEntity
{
    public string MemberName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public DateTime JoinDate { get; set; } = new DateTime();
    public string Notes { get; set; } = string.Empty;
    //public Position position { get; set; }
    public double lat { get; set; } = 0;
    public double lng { get; set; } = 0;
}