using Microsoft.AspNetCore.Http;
using SocialMedia.Application.DTOs.Post;
using SocialMedia.Application.Features.Commands.Post.Archive;
using SocialMedia.Application.Features.Commands.Post.Create;
using SocialMedia.Application.Features.Commands.Post.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Abstractions.Services
{
    public interface IPostService
    {
        Task<PostCreateCommandResponse> CreatePostAsync(CreatePostDto post);
        Task<EditPostCommandResponse> EditPostAsync(EditPostDto post);
        Task DeletePostImageAsync(string id);
        Task<bool> DeletePostAsync(string id);
        Task ToggleArchivePostAsync(string id);
    }
}
