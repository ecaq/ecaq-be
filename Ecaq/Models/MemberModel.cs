namespace Ecaq.Models;

public class MemberModel: BaseEntity
{
    public string MemberName { get; set; }
    public DateTime JoinDate { get; set; }
    public string Notes { get; set; }
    //public Position position { get; set; }
    public double lat { get; set; }
    public double lng { get; set; }
}