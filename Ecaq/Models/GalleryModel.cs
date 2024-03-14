using System.ComponentModel.DataAnnotations;

namespace Ecaq.Models
{
    public class GalleryModel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string Desc { get; set; } = string.Empty;
        public string ThumbUrl { get; set; } = string.Empty; // to do - to remove
        public string ImageUrl { get; set; } = string.Empty;
        public string PathUrl { get; set; } = string.Empty;
    }

    public class GalleryWithImagesDto : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string Desc { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string PathUrl { get; set; } = string.Empty;
        public List<string> ImageFileNames { get; set; } = new List<string>();
    }
}
