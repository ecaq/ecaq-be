using System.ComponentModel.DataAnnotations;

namespace Ecaq.Models
{
    public class SampleProfile : BaseEntity
    {
        public string? Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Desc { get; set; }
        public string? ThumbUrl { get; set; }
        public string? ImageUrl { get; set; }

        public string? PathUrl { get; set; }
        public string? ReservedString { get; set; }
    }
}
