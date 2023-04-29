using MediatR;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Follow.FollowRequest
{
    public class FollowRequestQueryHandler : IRequestHandler<FollowRequestQueryRequest, FollowRequestQueryResponse>
    {
        private readonly IFollowService _followService;
        public FollowRequestQueryHandler(IFollowService followService)
        {
            _followService = followService;
        }

        public async Task<FollowRequestQueryResponse> Handle(FollowRequestQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _followService.GetMyFollowRequestsAsync(request.Id,request.Page,request.Size);
            return new() { Succeeded = true, Value = res };
        }
    }
}
