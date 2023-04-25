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

        public async Task FollowUserAsync(string followerId, string followingId)
        {
            if (IsUsersValid(followerId, followingId))
            {
                if (!await IsAlreadyFollowingAsync(followerId, followingId))
                {
                    User followingUser = await _userManager.FindByIdAsync(followingId);
                    await WriteUsersAsync(followerId, followingId, followingUser.IsPrivate);
                }
                else
                {
                    Follow currentData = await _followReadRepo.GetAsync(x => x.FollowerId == followerId && x.FollowingId == followingId);
                    await _followWriteRepo.RemoveAsync(currentData.Id);
                }

                await _followWriteRepo.SaveAsync();
            }
        }

        public async Task<FollowingDto> MyFollowingsAsync(string id)
        {
            var followings = await _followReadRepo
                        .GetAllWhere(x => x.FollowerId == id && x.IsFollowing == true)
                        .Include(x=>x.Following)
                        .ThenInclude(x=>x.ProfileImage)
                        .Select(x=> new Following
                        {
                            UserName = x.Following.UserName,
                            ProfileImage = x.Following.ProfileImage.Path
                        })
                        .ToListAsync();

            return new() { FollowingCount = followings.Count, Followings = followings };
        }

        private async Task<bool> IsAlreadyFollowingAsync(string followerId, string followingId)
            => await _context.Follows.AnyAsync(x => x.FollowerId == followerId && x.FollowingId == followingId);

        private bool IsUsersValid(string followerId, string followingId) => followerId != followingId;

        private async Task WriteUsersAsync(string followerId, string followingId, bool isPrivate)
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
            }
        }

    }
}
