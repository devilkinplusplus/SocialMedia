using MediatR;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Rank.GetUserRanks
{
    public class GetUserRanksQueryHandler : IRequestHandler<GetUserRanksQueryRequest, GetUserRanksQueryResponse>
    {
        private readonly IRankService _rankService;
        public GetUserRanksQueryHandler(IRankService rankService)
        {
            _rankService = rankService;
        }

        public async Task<GetUserRanksQueryResponse> Handle(GetUserRanksQueryRequest request, CancellationToken cancellationToken)
        {
            return await _rankService.GetUserRanksAsync(request.UserId);
        }
    }
}
