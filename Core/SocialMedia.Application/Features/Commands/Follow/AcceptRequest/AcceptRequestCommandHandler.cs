using MediatR;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Follow.AcceptRequest
{
    public class AcceptRequestCommandHandler : IRequestHandler<AcceptRequestCommandRequest, AcceptRequestCommandResponse>
    {
        private readonly IFollowService _followService;
        public AcceptRequestCommandHandler(IFollowService followService)
        {
            _followService = followService;
        }

        public async Task<AcceptRequestCommandResponse> Handle(AcceptRequestCommandRequest request, CancellationToken cancellationToken)
        {
            await _followService.AcceptFollowRequestAsync(request.FollowerId, request.FollowingId);
            return new() { Succeeded = true };
        }
    }
}
