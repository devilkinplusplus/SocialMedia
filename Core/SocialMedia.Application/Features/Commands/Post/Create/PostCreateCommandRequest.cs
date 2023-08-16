using MediatR;
using Microsoft.AspNetCore.Http;
using SocialMedia.Application.DTOs.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Post.Create
{
    public class PostCreateCommandRequest : IRequest<PostCreateCommandResponse>
    {
        public string? Content { get; set; }
        public string? UserId { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
