using Microsoft.AspNetCore.Http;
using SocialMedia.Application.DTOs.Post;
using SocialMedia.Application.Features.Commands.Post.Archive;
using SocialMedia.Application.Features.Commands.Post.Create;
using SocialMedia.Application.Features.Commands.Post.Edit;
using SocialMedia.Application.Features.Queries.Post.Get;
using SocialMedia.Application.Features.Queries.Post.GetAll;
using SocialMedia.Application.Features.Queries.Post.GetMyPosts;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Abstractions.Services
{
    public interface IPostService
    {
        Task<PostCreateCommandResponse> CreatePostAsync(string content, string userId, IFormFileCollection files);
        Task<EditPostCommandResponse> EditPostAsync(string id,string content,IFormFileCollection files);
        Task DeletePostImageAsync(string id);
        Task<bool> DeletePostAsync(string id);
        Task ToggleArchivePostAsync(string id);
        Task<GetAllPostsQueryResponse> GetAllPostsAsync(string userId,int page = 0,int size = 5);
        Task<GetMyPostsQueryResponse> GetMyPostsAsync(string userId,string authId,int page = 0,int size = 5);
        Task<GetPostCommandResponse> GetPostAsync(string postId);
    }
}
