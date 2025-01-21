using SocialMediaBackend.Application.DTOs.PostImages;

namespace SocialMediaBackend.Application.DTOs.Posts
{
    public class PostListDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfilePhoto { get; set; }
        public bool IsLiked { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<PostImagesDto> PostImages { get; set; }
        
    }
}
