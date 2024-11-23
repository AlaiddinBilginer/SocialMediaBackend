using SocialMediaBackend.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaBackend.Domain.Entities
{
    public class PostImage : Entity
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Storage { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }

        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
    }
}
