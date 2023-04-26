using MediatR;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.Repositories.Follows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Follow.MyFollowings
{
    public class MyFollowingsQueryHandler : IRequestHandler<MyFollowingsQueryRequest, MyFollowingsQueryResponse>
    {
        private readonly IFollowService _followService;
        public MyFollowingsQueryHandler(IFollowService followService)
        {
            _followService = followService;
        }

        public async Task<MyFollowingsQueryResponse> Handle(MyFollowingsQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _followService.GetMyFollowingsAsync(request.Id);
            return new() { Succeeded = true, Value = res };
        }
    }
}
