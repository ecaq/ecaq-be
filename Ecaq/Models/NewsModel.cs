namespace Ecaq.Models;

public class NewsModel : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Intro { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime NewsDate { get; set; } = new DateTime();
}