﻿namespace SocialMediaBackend.Application.DTOs.Comments
{
    public class CommentDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfilePhoto { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<CommentDto> Replies { get; set; }
    }
}