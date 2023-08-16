using SocialMedia.Application.DTOs.Rank;
using SocialMedia.Application.Features.Commands.Ranks;
using SocialMedia.Application.Features.Queries.Rank.GetUserRanks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Abstractions.Services
{
    public interface IRankService
    {
        Task<AchieveRankCommandResponse> AchieveRankAsync(string userId);
        Task WriteRanksAsync(List<string> ranks,string userId);
        Task<GetUserRanksQueryResponse> GetUserRanksAsync(string userId);
    }
}
