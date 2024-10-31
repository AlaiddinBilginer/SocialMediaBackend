using SocialMediaBackend.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaBackend.Domain.Entities
{
    public class PostImage : Entity
    {
        public string FileName { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;

        public Guid PostId { get; set; }
        public Post Post { get; set; } = new Post();

        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
    }
}
