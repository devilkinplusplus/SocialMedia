using MediatR;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Post.Get
{
    public class GetPostCommandHandler : IRequestHandler<GetPostCommandRequest, GetPostCommandResponse>
    {
        private readonly IPostService _postService;
        public GetPostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<GetPostCommandResponse> Handle(GetPostCommandRequest request, CancellationToken cancellationToken)
        {
            return await _postService.GetPostAsync(request.PostId);
        }
    }
}
