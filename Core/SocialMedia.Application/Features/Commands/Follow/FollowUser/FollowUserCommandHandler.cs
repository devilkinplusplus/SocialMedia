using MediatR;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Follow.FollowUser
{
    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommandRequest, FollowUserCommandResponse>
    {
        private readonly IFollowService _followService;
        public FollowUserCommandHandler(IFollowService followService)
        {
            _followService = followService;
        }

        public async Task<FollowUserCommandResponse> Handle(FollowUserCommandRequest request, CancellationToken cancellationToken)
        {
            var res = await _followService.FollowUserAsync(request.FollowerId, request.FollowingId);
            return new() { Succeeded = true ,FollowState = res};
        }
    }
}
