using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.Consts;
using SocialMedia.Application.DTOs.Rank;
using SocialMedia.Application.Enums;
using SocialMedia.Application.Features.Commands.Ranks;
using SocialMedia.Application.Features.Queries.Rank.GetUserRanks;
using SocialMedia.Domain.Entities;
using SocialMedia.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Persistance.Services
{

    public class RankService : IRankService
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<string, Func<RankCalculateDto, bool>> rankConditions;
        public RankService(AppDbContext context)
        {
            _context = context;
            rankConditions = new Dictionary<string, Func<RankCalculateDto, bool>>
            {
                { nameof(RankType.Rocket), CanGetRocketRank },
                { nameof(RankType.Global), CanGetGlobalRank },
                { nameof(RankType.Star), CanGetStarRank },
                { nameof(RankType.Robotto), CanGetRobottoRank },
                { nameof(RankType.Ghost), CanGetGhostRank },
                { nameof(RankType.Skull), CanGetSkullRank }
            };
        }


        public async Task<AchieveRankCommandResponse> AchieveRankAsync(string userId)
        {
            RankCalculateDto calculate = await CalculateAsync(userId);
            RankDto achievedRanks = new RankDto();
            achievedRanks.Names = new List<string>();
            bool canUserGetRank = false;

            foreach (var kvp in rankConditions)
            {
                if (kvp.Value(calculate))
                {
                    canUserGetRank = true;
                    achievedRanks.Names.Add(kvp.Key);
                }
            }
            if (canUserGetRank == true)
            {
                await WriteRanksAsync(achievedRanks.Names, userId);
                return new() { Succeeded = true, Value = achievedRanks };
            }

            return new() { Succeeded = false, Errors = new List<string>() { Messages.CantGetRanks } };
        }

        public async Task WriteRanksAsync(List<string> ranks, string userId)
        {
            List<string> rankIds = new();
            foreach (var rank in ranks)
            {
                string id = await _context.Ranks.Where(x => x.Name == rank).Select(x => x.Id).FirstOrDefaultAsync();

                bool combinationExists = await HasUserAlreadyRank(userId, id);

                if (combinationExists) continue;

                await _context.UserRanks.AddAsync(new()
                {
                    UserId = userId,
                    RankId = id
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<GetUserRanksQueryResponse> GetUserRanksAsync(string userId)
        {
            List<string> ranks = await _context.UserRanks.Where(x => x.UserId == userId)
                                                                 .Include(x => x.Rank)
                                                                 .Select(x => x.Rank.Name)
                                                                 .ToListAsync();

            if (!ranks.Any())
                return new() { Succeeded = false, Errors = new List<string> { Messages.NoRanksFoundMessage } };

            return new() { Succeeded = true, Value = new RankDto { Names = ranks } };
        }

        private async Task<bool> HasUserAlreadyRank(string userId, string rankId)
            => await _context.UserRanks.AnyAsync(x => x.UserId == userId && x.RankId == rankId);

        private async Task<RankCalculateDto> CalculateAsync(string userId)
        {
            RankCalculateDto calculate = new();

            calculate.Posts = await _context.Posts.Where(x => x.UserId == userId && x.IsDeleted == false).CountAsync();
            calculate.Likes = await _context.PostReactions
                                    .Where(pr => pr.Post.UserId == userId && pr.IsLike && pr.UserId != userId && !pr.Post.IsDeleted)
                                    .CountAsync();

            calculate.Followers = await _context.Follows.Where(x => x.FollowingId == userId && x.IsFollowing == true).CountAsync();
            calculate.Followings = await _context.Follows.Where(x => x.FollowerId == userId && x.IsFollowing == true).CountAsync();

            return calculate;
        }

        #region Helper Methods
        private bool CanGetRocketRank(RankCalculateDto calculate)
            => (calculate.Followers >= 10 && calculate.Followings >= 5 && calculate.Posts >= 5);

        private bool CanGetGlobalRank(RankCalculateDto calculate)
            => (calculate.Followers >= 20 && calculate.Followings >= 15 && calculate.Posts >= 15);

        private bool CanGetStarRank(RankCalculateDto calculate)
            => (calculate.Followers >= 30 && calculate.Followings >= 30 && calculate.Likes >= 30);

        private bool CanGetRobottoRank(RankCalculateDto calculate)
            => (calculate.Followers >= 50 && calculate.Likes >= 40 && calculate.Posts >= 15);

        private bool CanGetGhostRank(RankCalculateDto calculate)
            => (calculate.Followers >= 50 && calculate.Followings >= 10 && calculate.Posts == 0);

        private bool CanGetSkullRank(RankCalculateDto calculate)
            => (calculate.Followers >= 100 && calculate.Followings >= 50 && calculate.Likes >= 70);

        #endregion
    }
}
