using MediatR;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.DTOs.Rank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Ranks
{
    public class AchieveRankCommandHandler : IRequestHandler<AchieveRankCommandRequest, AchieveRankCommandResponse>
    {
        private readonly IRankService _rankService;
        public AchieveRankCommandHandler(IRankService rankService)
        {
            _rankService = rankService;
        }

        public async Task<AchieveRankCommandResponse> Handle(AchieveRankCommandRequest request, CancellationToken cancellationToken)
        {
             return await _rankService.AchieveRankAsync(request.UserId);
        }
    }
}
