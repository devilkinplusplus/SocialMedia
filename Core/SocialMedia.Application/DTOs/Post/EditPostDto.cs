using Microsoft.AspNetCore.Http;


namespace SocialMedia.Application.DTOs.Post
{
    public class EditPostDto
    {
        public string Id { get; set; }
        public string? Content { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
