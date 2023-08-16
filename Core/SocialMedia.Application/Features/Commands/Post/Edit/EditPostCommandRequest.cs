using MediatR;
using Microsoft.AspNetCore.Http;
using SocialMedia.Application.DTOs.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Post.Edit
{
    public class EditPostCommandRequest : IRequest<EditPostCommandResponse>
    {
        public string Id { get; set; }
        public string? Content { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
