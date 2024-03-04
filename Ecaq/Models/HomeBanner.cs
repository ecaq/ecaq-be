using System.ComponentModel.DataAnnotations;

namespace Ecaq.Models
{
    public class HomeBanner : BaseEntity
    {
        public string? Name { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string? Desc { get; set; } = string.Empty;
        public string? ThumbUrl { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;

        public string? ButtonText { get; set; } = string.Empty;
        public string? ButtonUrl { get; set; } = string.Empty;
    }
}
