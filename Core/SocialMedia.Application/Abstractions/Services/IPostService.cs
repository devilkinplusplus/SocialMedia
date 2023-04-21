using Microsoft.AspNetCore.Http;
using SocialMedia.Application.DTOs.Post;
using SocialMedia.Application.Features.Commands.Post.Create;
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
    }
}
