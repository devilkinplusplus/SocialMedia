using MediatR;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Follow.MyFollowers
{
    public class GetMyFollowersQueryHandler : IRequestHandler<GetMyFollowersQueryRequest, GetMyFollowersQueryResponse>
    {
        private readonly IFollowService _followService;
        public GetMyFollowersQueryHandler(IFollowService followService)
        {
            _followService = followService;
        }

        public async Task<GetMyFollowersQueryResponse> Handle(GetMyFollowersQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _followService.GetMyFollowersAsync(request.Id,request.Page,request.Size);
            return new() { Succeeded = true,Value = res};
        }
    }
}
