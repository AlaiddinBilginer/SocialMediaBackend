using SocialMediaBackend.Application.DTOs.PostImages;
using SocialMediaBackend.Application.DTOs.Tags;
using SocialMediaBackend.Domain.Entities;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetByIdPost
{
    public class GetByIdPostQueryResponse
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string? UserPhoto { get; set; }
        public ICollection<PostImagesDto> PostImages { get; set; }
        public ICollection<TagDto> Tags { get; set; }
    }
}
