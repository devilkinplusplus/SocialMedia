using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.DTOs.Follow;
using SocialMedia.Application.Repositories.Follows;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Entities.Identity;
using SocialMedia.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Persistance.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowWriteRepository _followWriteRepo;
        private readonly IFollowReadRepository _followReadRepo;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        public FollowService(IFollowReadRepository followReadRepo, IFollowWriteRepository followWriteRepo, UserManager<User> userManager, AppDbContext context)
        {
            _followReadRepo = followReadRepo;
            _followWriteRepo = followWriteRepo;
            _userManager = userManager;
            _context = context;
        }

        public async Task AcceptFollowRequestAsync(string followerId, string followingId)
        {
            Follow? value = await _followReadRepo
                    .GetAsync(x => x.FollowerId == followerId && x.FollowingId == followingId && x.HasRequest == true);

            value.IsFollowing = true;
            value.HasRequest = false;

            await _followWriteRepo.SaveAsync();
        }

        public async Task<FollowState> FollowUserAsync(string followerId, string followingId)
        {
            if (IsUsersValid(followerId, followingId))
            {
                if (!await IsAlreadyFollowingAsync(followerId, followingId))
                {
                    User followingUser = await _userManager.FindByIdAsync(followingId);
                    var res = await WriteUsersAsync(followerId, followingId, followingUser.IsPrivate);
                    await _followWriteRepo.SaveAsync();

                    var followingUs = await GetFollowingAsync(followingId, followerId);
                    var followerUs = await GetFollowerAsync(followingId, followerId);
                    res.Follower = followerUs;
                    res.Following = followingUs;
                    return res;
                }
                else
                {
                    Follow currentData = await _followReadRepo.GetAsync(x => x.FollowerId == followerId && x.FollowingId == followingId);
                    var followingUs = await GetFollowingAsync(followingId, followerId);
                    var followerUs = await GetFollowerAsync(followingId, followerId);
                    await _followWriteRepo.RemoveAsync(currentData.Id);
                    await _followWriteRepo.SaveAsync();
                    return new() { IsUnfollowed = true, Follower = followerUs, Following = followingUs };
                }
            }
            throw new Exception("What the hell is your problem ?");
        }

        public async Task<FollowingDto> GetMyFollowersAsync(string id, int page = 0, int size = 5)
        {
            var followings = await _followReadRepo
                         .GetAllWhere(x => x.FollowingId == id && x.IsFollowing == true)
                         .Include(x => x.Follower)
                         .ThenInclude(x => x.ProfileImage)
                         .Select(x => new Following
                         {
                             Id = x.Follower.Id,
                             FirstName = x.Follower.FirstName,
                             LastName = x.Follower.LastName,
                             UserName = x.Follower.UserName,
                             ProfileImage = x.Follower.ProfileImage.Path,
                             FollowerId = x.FollowingId,
                         })
                         .Skip(page * size)
                         .Take(size)
                         .ToListAsync();

            foreach (var user in followings)
            {
                user.DoIFollow = DoIFollow(user.FollowerId,user.Id);
            }

            int followingCount = await _followReadRepo.GetAllWhere(x => x.FollowingId == id && x.IsFollowing == true).CountAsync();

            return new() { FollowingCount = followingCount, Followings = followings };
        }

        public async Task<FollowingDto> GetMyFollowingsAsync(string id, int page = 0, int size = 5)
        {
            var followings = await _followReadRepo
                        .GetAllWhere(x => x.FollowerId == id && x.IsFollowing == true)
                        .Include(x => x.Following)
                        .ThenInclude(x => x.ProfileImage)
                        .Select(x => new Following
                        {
                            Id = x.Following.Id,
                            FirstName = x.Following.FirstName,
                            LastName = x.Following.LastName,
                            UserName = x.Following.UserName,
                            ProfileImage = x.Following.ProfileImage.Path,
                            FollowerId = x.FollowerId
                        })
                        .Skip(page * size)
                        .Take(size)
                        .ToListAsync();



            int followingCount = await _followReadRepo.GetAllWhere(x => x.FollowerId == id && x.IsFollowing == true).CountAsync();

            return new() { FollowingCount = followingCount, Followings = followings };
        }

        public async Task<FollowingDto> GetMyFollowRequestsAsync(string id, int page = 0, int size = 5)
        {
            var followings = await _followReadRepo
                        .GetAllWhere(x => x.FollowingId == id && x.HasRequest == true)
                        .Include(x => x.Follower)
                        .ThenInclude(x => x.ProfileImage)
                        .Select(x => new Following
                        {
                            FirstName = x.Follower.FirstName,
                            LastName = x.Follower.LastName,
                            UserName = x.Follower.UserName,
                            ProfileImage = x.Follower.ProfileImage.Path
                        })
                        .Skip(page * size)
                        .Take(size)
                        .ToListAsync();

            return new() { FollowingCount = followings.Count, Followings = followings };
        }

        private async Task<bool> IsAlreadyFollowingAsync(string followerId, string followingId)
            => await _context.Follows.AnyAsync(x => x.FollowerId == followerId && x.FollowingId == followingId);

        private bool IsUsersValid(string followerId, string followingId) => followerId != followingId;

        private async Task<FollowState> WriteUsersAsync(string followerId, string followingId, bool isPrivate)
        {
            if (isPrivate)
            {
                await _followWriteRepo.AddAsync(new()
                {
                    Id = Guid.NewGuid().ToString(),
                    FollowerId = followerId,
                    FollowingId = followingId,
                    HasRequest = true
                });
                return new() { HasRequest = true, IsFollowing = false };
            }
            else
            {
                await _followWriteRepo.AddAsync(new()
                {
                    Id = Guid.NewGuid().ToString(),
                    FollowerId = followerId,
                    FollowingId = followingId,
                    IsFollowing = true
                });
                return new() { HasRequest = false, IsFollowing = true };
            }
        }

        private async Task<Following> GetFollowingAsync(string followingId, string followerId)
        {
            var value = await _followReadRepo.GetAsync(x => x.FollowerId == followerId && x.FollowingId == followingId, "Following.ProfileImage", "Follower");
            var following = new Following()
            {
                Id = followingId,
                FollowerId = followerId,
                FirstName = value.Following.FirstName,
                LastName = value.Following.LastName,
                UserName = value.Following.UserName,
                ProfileImage = value.Following.ProfileImage?.Path
            };
            return following;
        }
        private async Task<Following> GetFollowerAsync(string followingId, string followerId)
        {
            var value = await _followReadRepo.GetAsync(x => x.FollowerId == followerId && x.FollowingId == followingId, "Follower.ProfileImage", "Following");
            var follower = new Following()
            {
                Id = followingId,
                FollowerId = followerId,
                FirstName = value.Follower.FirstName,
                LastName = value.Follower.LastName,
                UserName = value.Follower.UserName,
                ProfileImage = value.Follower.ProfileImage?.Path
            };
            return follower;
        }
        private  bool DoIFollow(string followerId, string followingId) =>
             _context.Follows.Where(x => x.FollowerId == followerId && x.FollowingId == followingId && x.IsFollowing == true).Any();

    }
}
