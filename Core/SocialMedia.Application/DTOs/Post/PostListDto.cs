using SocialMedia.Application.DTOs.User;
using SocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.DTOs.Post
{
    public class PostListDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserListDto User { get; set; }
        public string? Content { get; set; }
        public IEnumerable<string>? Files { get; set; }
        public IEnumerable<Domain.Entities.Comment>? Comments { get; set; }
        public int Likes { get; set; }
        public bool IsLiked { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<FileDto> FileInfos { get; set; }
    }
    public class FileDto
    {
        public string FileId { get; set; }
        public string  Path { get; set; }
    }
}
