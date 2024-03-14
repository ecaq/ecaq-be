using System.ComponentModel.DataAnnotations;

namespace Ecaq.Models
{
    public class HomeBanner : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string Desc { get; set; } = string.Empty;
        public string ImageMobile { get; set; } = string.Empty;
        public string ImageDesktop { get; set; } = string.Empty;

        public string ButtonText { get; set; } = string.Empty;
        public string ButtonUrl { get; set; } = string.Empty;
        public bool ButtonUrlExternal { get; set; } = false;

        public string VideoUrl { get; set; } = string.Empty;
        public string VideoDuration { get; set; } = string.Empty;
        public bool IsVideo { get; set; } = false;
    }
}
