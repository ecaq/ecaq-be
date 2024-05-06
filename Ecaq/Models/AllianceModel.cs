using System.ComponentModel.DataAnnotations.Schema;

namespace Ecaq.Models
{
    public class AllianceModel: BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public virtual List<AllianceCollectionModel>? AllianceCollectionModels { get; set; }
    }

    public class AllianceCollectionModel: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Desc { get; set; }= string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public int AllianceModelId { get; set; }
        [ForeignKey("AllianceModelId")]
        public virtual AllianceModel AllianceModel { get; set; }
    }
}
