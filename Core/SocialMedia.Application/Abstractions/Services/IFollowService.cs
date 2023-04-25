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

        Task<FollowingDto> MyFollowingsAsync(string id);

    }
}
