using SocialMedia.Application.DTOs.Follow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Abstractions.Services
{
    public interface IFollowService
    {
        Task FollowUserAsync(string followerId,string followingId);
        Task<FollowingDto> GetMyFollowingsAsync(string id);
        Task<FollowingDto> GetMyFollowersAsync(string id);
        Task<FollowingDto> GetMyFollowRequestsAsync(string id);
        Task AcceptFollowRequestAsync(string followerId,string followingId);

    }
}
