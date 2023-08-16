using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Comment.SoftDelete
{
    public class SoftDeleteCommentRequest : IRequest<SoftDeleteCommentResponse>
    {
        public string Id { get; set; }
    }
}
