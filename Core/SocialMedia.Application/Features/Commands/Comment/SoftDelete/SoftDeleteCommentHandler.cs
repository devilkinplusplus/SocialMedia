using MediatR;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Comment.SoftDelete
{
    public class SoftDeleteCommentHandler : IRequestHandler<SoftDeleteCommentRequest, SoftDeleteCommentResponse>
    {
        private readonly ICommentService _commentService;

        public SoftDeleteCommentHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<SoftDeleteCommentResponse> Handle(SoftDeleteCommentRequest request, CancellationToken cancellationToken)
        {
            await _commentService.SoftDeleteCommentAsync(request.Id);
            return new() { Succeeded = true };
        }
    }
}
